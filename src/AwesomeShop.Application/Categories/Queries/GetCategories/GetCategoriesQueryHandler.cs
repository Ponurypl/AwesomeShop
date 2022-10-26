using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Categories.Queries.GetCategories;

public sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(ICategoriesRepository categoriesRepository, IMapper mapper)
    {
        _categoriesRepository = categoriesRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.GetAllAsync();

        return _mapper.Map<List<CategoryDto>>(categories);
    }
}