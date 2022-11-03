using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.AddProductToCart;

public sealed class AddProductToCartCommandHandler : ICommandHandler<AddProductToCartCommand>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductToCartCommandHandler(IOrdersRepository ordersRepository, IUsersRepository usersRepository,
        IProductsRepository productsRepository, IUnitOfWork unitOfWork)
    {
        _ordersRepository = ordersRepository;
        _usersRepository = usersRepository;
        _productsRepository = productsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(AddProductToCartCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity <= 0)
        {
            return Result.Failure(Failures.InvalidQuantity);
        }

        var userId = new UserId(request.UserId);
        var order = await _ordersRepository.GetCartOrderByUserIdAsync(userId, cancellationToken);
        if (order is null)
        {
            var customer = await _usersRepository.GetByIdAsync(userId, cancellationToken);
            if (customer is null)
            {
                return Result.Failure(Failures.InvalidUsername);
            }
            order = Order.Create(customer.Id);
            await _ordersRepository.AddAsync(order, cancellationToken);
        }

        var product = await _productsRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);
        if (product is null)
        {
            return Result.Failure(Failures.InvalidProduct);
        }

        order.AddOrderItem(product.Id, product.Name, request.Quantity, product.Price);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}