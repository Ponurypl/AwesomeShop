using MediatR;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Messaging;

internal interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{

}