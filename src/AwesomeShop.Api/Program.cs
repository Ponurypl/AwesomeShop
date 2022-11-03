using OnboardingIntegrationExample.AwesomeShop.Api;
using OnboardingIntegrationExample.AwesomeShop.Application;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure;
using System.Text.Json;
using FastEndpoints.ClientGen;
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
                       }, shortSchemaNames: true);
services.AddCors(setup => setup.AddDefaultPolicy(policyBuilder =>
                                                     policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
                                                                  .Build()));
var app = builder.Build();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c =>
                     {
                         c.Endpoints.RoutePrefix = "api";
                         c.Endpoints.ShortNames = true;
                         c.Versioning.PrependToRoute = true;
                         c.Versioning.Prefix = "v";
                         c.Versioning.DefaultVersion = 1;
                         c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                     });

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

if (app.Environment.IsDevelopment())
{
    app.MapCSharpClientEndpoint("/cs-client", "V1", s =>
                                                    {
                                                        s.ClassName = "ApiClient";
                                                        s.CSharpGeneratorSettings.Namespace =
                                                            "OnboardingIntegrationExample.AwesomeShop.Api.Client";
                                                    });
}

app.Run();
