﻿using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts.Dto;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts;

public sealed class GetCategoryProductsQueryHandler : IQueryHandler<GetCategoryProductsQuery, List<ProductDto>>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IMapper _mapper;

    public GetCategoryProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ProductDto>>> Handle(GetCategoryProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productsRepository.GetByCategoryAsync(request.categoryId);

        return _mapper.Map<List<ProductDto>>(products);
    }
}