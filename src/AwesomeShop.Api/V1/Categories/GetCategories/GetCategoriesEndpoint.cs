using OnboardingIntegrationExample.AwesomeShop.Application.Categories.Queries.GetCategories;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Categories.GetCategories;

public class GetCategoriesEndpoint : EndpointWithoutRequest<GetCategoriesResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetCategoriesEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("categories");
        Version(1);
    }
    
    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = await _sender.Send(new GetCategoriesQuery(), ct);
        if (result.IsFailure)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(new GetCategoriesResponse(_mapper.Map<List<GetCategoriesResponse.Category>>(result.Value)), ct);
    }
}