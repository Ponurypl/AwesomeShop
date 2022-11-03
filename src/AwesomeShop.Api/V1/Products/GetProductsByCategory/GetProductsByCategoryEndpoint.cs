using OnboardingIntegrationExample.AwesomeShop.Application.Products.Queries.GetCategoryProducts;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Products.GetProductsByCategory;

public class GetProductsByCategoryEndpoint : Endpoint<GetProductsByCategoryRequest, GetProductsByCategoryResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetProductsByCategoryEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("products");
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(GetProductsByCategoryRequest req, CancellationToken ct)
    {
        var response = await _sender.Send(new GetCategoryProductsQuery(req.CategoryId), ct);
        if (response.IsFailure)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var products = _mapper.Map<List<GetProductsByCategoryResponse.Product>>(response.Value);
        await SendOkAsync(new GetProductsByCategoryResponse(products), ct);
    }
}