using MediatR;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Common.Messaging;

internal interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{

}

internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
    
}