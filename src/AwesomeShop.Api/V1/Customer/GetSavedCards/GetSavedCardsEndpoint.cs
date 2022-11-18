using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetSavedCardDetails;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Customer.GetSavedCards;

public sealed class GetSavedCardsEndpoint : EndpointWithoutRequest<List<SavedCardDetails>>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetSavedCardsEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("customer/saved-cards");
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId))
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        var result = await _sender.Send(new GetSavedCardDetailsQuery(userId), ct);
        if (result.IsFailure)
        {
            ThrowError(result.Error.Message);
            return;
        }

        await SendOkAsync(_mapper.Map<List<SavedCardDetails>>(result.Value), ct);
    }
}