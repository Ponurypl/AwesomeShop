﻿using Microsoft.Extensions.Logging;
using OnboardingIntegrationExample.AwesomeShop.Application.Common;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Services;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

public sealed class CartCheckoutCommandHandler : ICommandHandler<CartCheckoutCommand, OrderDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNumberGenService _orderNumberGenService;
    private readonly IDateTime _dateTime;
    private readonly IMapper _mapper;
    private readonly IPaymentApiService _paymentApiService;
    private readonly ILogger<CartCheckoutCommandHandler> _logger;

    public CartCheckoutCommandHandler(IOrdersRepository ordersRepository, IUsersRepository usersRepository,
                                      IUnitOfWork unitOfWork, IOrderNumberGenService orderNumberGenService, IDateTime dateTime,
                                      IMapper mapper, IPaymentApiService paymentApiService, ILogger<CartCheckoutCommandHandler> logger)
    {
        _ordersRepository = ordersRepository;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _orderNumberGenService = orderNumberGenService;
        _dateTime = dateTime;
        _mapper = mapper;
        _paymentApiService = paymentApiService;
        _logger = logger;
    }

    public async Task<Result<OrderDto>> Handle(CartCheckoutCommand request, CancellationToken cancellationToken)
    {
        string? redirectUrl = null;
        var customerId = new UserId(request.UserId);
        var cart = await _ordersRepository.GetCartOrderByUserIdAsync(customerId, cancellationToken);

        if (cart is null)
        {
            return Result.Failure<OrderDto>(Failures.NoOpenCart);
        }

        var orderNumber = await _orderNumberGenService.GenerateOrderNumberAsync(cancellationToken);

        cart.ProcessCheckout(orderNumber, request.FirstName, request.LastName, request.AddressLine1, request.AddressLine2,
                             request.City, request.ZipCode, request.PhoneNumber, _dateTime.UtcNow, request.CheckoutId);
        
        switch (request.PaymentMethod)
        {
            case PaymentMethods.CardSale:
                try
                {
                    var paymentStatus = await _paymentApiService.PayByCardAsync(customerId, cart.Summary, request.CardDetails!);
            
                    cart.SetWaitingForPaymentStatus();
                    cart.AddPaymentId(paymentStatus.PaymentId);

                    if (paymentStatus.TokenId is not null)
                    {
                        var user = await _usersRepository.GetByIdAsync(customerId, cancellationToken);
                        user!.AddPaymentToken(paymentStatus.TokenId);
                    }

                    if (paymentStatus.StatusCode == 9)
                    {
                        cart.SetPaidStatus();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Cart payment was unsuccessful due to an error");
                }

                break;
            case PaymentMethods.CardAuthorization:
                try
                {
                    var paymentStatus =
                        await _paymentApiService.SetupDelayedCardPaymentAsync(customerId, cart.Summary,
                                                                         request.CardDetails!);

                    cart.SetWaitingForPaymentStatus();
                    cart.AddPaymentId(paymentStatus.PaymentId);

                    if (paymentStatus.TokenId is not null)
                    {
                        var user = await _usersRepository.GetByIdAsync(customerId, cancellationToken);
                        user!.AddPaymentToken(paymentStatus.TokenId);
                    }

                    if (paymentStatus.StatusCode == 5)
                    {
                        await _paymentApiService.CaptureDelayedCardPaymentAsync(paymentStatus.RawPaymentId, cart.Summary);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Cart payment was unsuccessful due to an error");
                }
                break;
            case PaymentMethods.CardDontCapture:
                try
                {
                    var paymentStatus =
                        await _paymentApiService.SetupDelayedCardPaymentAsync(customerId, cart.Summary,
                                                                              request.CardDetails!);

                    cart.SetWaitingForPaymentStatus();
                    cart.AddPaymentId(paymentStatus.PaymentId);

                    if (paymentStatus.TokenId is not null)
                    {
                        var user = await _usersRepository.GetByIdAsync(customerId, cancellationToken);
                        user!.AddPaymentToken(paymentStatus.TokenId);
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Cart payment was unsuccessful due to an error");
                }
                break;
            case PaymentMethods.SavedCardSale:
                try
                {
                    var user = await _usersRepository.GetByIdAsync(customerId, cancellationToken);
                    var token = user!.CardAliases.FirstOrDefault(a => a.Id == request.SavedCard!.Id);

                    if (token is null)
                    {
                        Result.Failure<OrderDto>(Failures.InvalidToken);
                    }

                    var paymentStatus =
                        await _paymentApiService.PayByTokenAsync(customerId, cart.Summary, token!.TokenId,
                                                            request.SavedCard!.CVV);

                    cart.SetWaitingForPaymentStatus();
                    cart.AddPaymentId(paymentStatus.PaymentId);

                    if (paymentStatus.StatusCode == 9)
                    {
                        cart.SetPaidStatus();
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Cart payment was unsuccessful due to an error");
                }
                break;
            case PaymentMethods.HostedSale:
                try
                {
                    var paymentStatus = await _paymentApiService.CreateHostedSalePaymentAsync(customerId, cart.Summary, request.RedirectUrl!);

                    cart.SetWaitingForPaymentStatus();
                    redirectUrl = paymentStatus.RedirectUrl;

                    cart.AddHostedPaymentId(paymentStatus.Id);

                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Cart payment was unsuccessful due to an error");
                }
                break;
            case PaymentMethods.HostedAuthorization:
                try
                {
                    var paymentStatus = await _paymentApiService.CreateHostedAuthorizationPaymentAsync(customerId, cart.Summary, request.RedirectUrl!);

                    cart.SetWaitingForPaymentStatus();
                    redirectUrl = paymentStatus.RedirectUrl;

                    cart.AddHostedPaymentId(paymentStatus.Id);

                }
                catch (Exception e)
                {
                    _logger.LogWarning(e, "Cart payment was unsuccessful due to an error");
                }
                break;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success(new OrderDto()
                              {
                                  Id = cart.Id.Value,
                                  Number = cart.Number!,
                                  Status = cart.Status.ToString(),
                                  Summary = cart.Summary,
                                  RedirectUrl = redirectUrl
                              });
    }
}