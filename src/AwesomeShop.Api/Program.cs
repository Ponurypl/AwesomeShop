using OnboardingIntegrationExample.AwesomeShop.Api;
using OnboardingIntegrationExample.AwesomeShop.Application;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure;
using System.Text.Json;
using FastEndpoints.Swagger;
using FastEndpoints.Security;
using OnboardingIntegrationExample.AwesomeShop.Api.Common.Authentication;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddApplicationLayer();
services.AddInfrastructureLayer(builder.Configuration);
services.AddApiLayerServices();

services.AddFastEndpoints();
services.AddAuthenticationJWTBearer(AuthDefaults.JwtSigningKey);

services.AddSwaggerDoc(maxEndpointVersion: 1, settings: s =>
                       {
                           s.DocumentName = "V1";
                           s.Title = "AwesomeShop Api";
                           s.Version = "v1.0";
                       });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c =>
                     {
                         c.Endpoints.RoutePrefix = "api";
                         c.Endpoints.ShortNames = false;
                         c.Versioning.PrependToRoute = true;
                         c.Versioning.Prefix = "v";
                         c.Versioning.DefaultVersion = 1;
                         c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                     });

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.Run();
