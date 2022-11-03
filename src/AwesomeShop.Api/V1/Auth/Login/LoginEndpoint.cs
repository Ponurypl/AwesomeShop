using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Api.Common.Authentication;
using OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;
using OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.VerifyCustomer;

namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Auth.Login;

public class LoginEndpoint : Endpoint<LoginRequest, LoginResponse>
{
    private readonly ISender _sender;

    public LoginEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("auth/login");
        AllowAnonymous();
        Version(1);
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var response = await _sender.Send(new VerifyCustomerQuery(req.Username, req.Password), ct);

        if (response.IsSuccess)
        {
            var expireAt = DateTime.UtcNow.AddDays(1);
            var jwtToken = JWTBearer.CreateToken(signingKey: AuthDefaults.JwtSigningKey,
                                                 expireAt: expireAt,
                                                 claims: new[]
                                                         {
                                                             ("Username", response.Value.Username),
                                                             ("UserId", response.Value.Id.ToString())
                                                         });

            await SendOkAsync(new LoginResponse(jwtToken, expireAt), ct);
        }
        else
        {
            ThrowError("The supplied credentials are invalid!");
        }
    }
}