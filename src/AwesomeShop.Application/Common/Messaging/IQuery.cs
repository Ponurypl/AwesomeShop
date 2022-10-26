using MediatR;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Messaging;

internal interface IQuery<TResponse> : IRequest<Result<TResponse>>
{

}