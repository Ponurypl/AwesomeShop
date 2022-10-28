using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart.Dto;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Queries.GetCurrentCart;

public sealed class GetCurrentCartQueryHandler : IQueryHandler<GetCurrentCartQuery, CartDto>
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IPhotosRepository _photosRepository;
    private readonly IMapper _mapper;

    public GetCurrentCartQueryHandler(IOrdersRepository ordersRepository, IPhotosRepository photosRepository,
                                      IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _photosRepository = photosRepository;
        _mapper = mapper;
    }

    public async Task<Result<CartDto>> Handle(GetCurrentCartQuery request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetCartOrderByUsernameAsync(request.Username, cancellationToken);
        if (order is null)
        {
            return Result.Failure<CartDto>(Failures.NoOpenCart);
        }

        var cartDto = _mapper.Map<CartDto>(order);
        foreach (var cartItem in cartDto.Items)
        {
            var thumbnail = await _photosRepository.GetThumbnailByProductIdAsync(new ProductId(cartItem.ProductId),
                                 cancellationToken);
            cartItem.ThumbnailFilename = thumbnail?.FileName;
        }

        return Result.Success(cartDto);
    }
}