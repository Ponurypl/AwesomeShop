using MediatR;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Messaging;

internal interface ICommand : IRequest<Result>
{
    
}

internal interface ICommand<TResponse> : IRequest<Result<TResponse>>
{

}