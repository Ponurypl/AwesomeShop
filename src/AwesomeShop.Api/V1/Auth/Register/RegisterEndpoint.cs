using Microsoft.AspNetCore.Http;
using OnboardingIntegrationExample.AwesomeShop.Application.Customers.Commands.RegisterCustomer;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Auth.Register;

public sealed class RegisterEndpoint : Endpoint<RegisterRequest>
{
    private readonly ISender _sender;

    public RegisterEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("auth/register");
        AllowAnonymous();
        Version(1);
        Description(b =>
                    {
                        b.Accepts<RegisterRequest>("application/json");
                        b.Produces(StatusCodes.Status204NoContent);
                    }, true);
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var command = new RegisterCustomerCommand(req.Username, req.Password, req.FirstName,
                                                  req.LastName, req.EmailAddress);

        var response = await _sender.Send(command, ct);

        if (response.IsFailure)
        {
            ThrowError(response.Error!.Message);
            return;
        }

        await SendNoContentAsync(ct);
    }
}