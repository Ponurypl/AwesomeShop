using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetProduct.Dto;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetProduct;

public sealed class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IPhotosRepository _photosRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductsRepository productsRepository, IPhotosRepository photosRepository,
                                  IMapper mapper)
    {
        _productsRepository = productsRepository;
        _photosRepository = photosRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _productsRepository.GetByIdAsync(new ProductId(request.ProductId), cancellationToken);
        if (product is null)
        {
            return Result.Failure<ProductDto>(Failures.ProductNotFound);
        }

        var dto = _mapper.Map<ProductDto>(product);

        var thumbnail = await _photosRepository.GetThumbnailByProductIdAsync(product.Id, cancellationToken);
        dto.ThumbnailFileName = thumbnail?.FileName;

        var photos = await _photosRepository.GetPhotosByProductIdAsync(product.Id, cancellationToken);
        foreach (var photo in photos)
        {
            dto.PhotoFileNames.Add(photo.FileName);
        }

        return Result.Success(dto);
    }
}