﻿using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;
using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Models;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Services;

public interface IPaymentApiService
{
    Task<PaymentDetails> PayByCardAsync(UserId customerId, double paymentAmount, CardDetails cardDetails);
    Task<PaymentDetails> SetupDelayedCardPaymentAsync(UserId customerId, double paymentAmount, CardDetails cardDetails);
    Task<PaymentDetails> CaptureDelayedCardPaymentAsync(string paymentId, double paymentAmount);
    Task<TokenDetails> GetTokenInfoAsync(string tokenId);
    Task<PaymentDetails> PayByTokenAsync(UserId customerId, double paymentAmount, string tokenId, string cvv);
    Task<HostedCheckoutDetails> CreateHostedSalePaymentAsync(UserId customerId, double paymentAmount, string returnUrl);

    Task<HostedCheckoutDetails> CreateHostedAuthorizationPaymentAsync(UserId customerId, double paymentAmount,
                                                                      string returnUrl);
    Task<PaymentDetails> GetHostedCheckoutStatusAsync(string hostedPaymentId);
}