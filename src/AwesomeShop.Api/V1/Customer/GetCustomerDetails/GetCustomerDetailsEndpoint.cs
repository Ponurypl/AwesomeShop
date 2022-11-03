using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Customer.GetCustomerDetails;

public sealed class GetCustomerDetailsEndpoint : EndpointWithoutRequest<GetCustomerDetailsResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetCustomerDetailsEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("customer");
        Version(1);
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        if (!Guid.TryParse(User.ClaimValue("UserId"), out var userId)) throw new NullReferenceException();

        var response = await _sender.Send(new GetCustomerQuery(userId), ct);

        if (response.IsFailure)
        {
            ThrowError(response.Error!.Message);
        }

        await SendOkAsync(_mapper.Map<GetCustomerDetailsResponse>(response.Value), ct);
    }
}