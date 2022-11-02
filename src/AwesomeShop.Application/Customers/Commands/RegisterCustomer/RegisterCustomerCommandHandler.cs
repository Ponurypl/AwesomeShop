using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Commands.RegisterCustomer;

public sealed class RegisterCustomerCommandHandler : ICommandHandler<RegisterCustomerCommand>
{
    private readonly ICryptoService _cryptoService;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCustomerCommandHandler(ICryptoService cryptoService, IUsersRepository usersRepository, IUnitOfWork unitOfWork)
    {
        _cryptoService = cryptoService;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _usersRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (existingUser is not null)
        {
            return Result.Failure(Failures.UsernameAlreadyTaken);
        }
        
        existingUser = await _usersRepository.GetByEmailAsync(request.EmailAddress, cancellationToken);
        if (existingUser is not null)
        {
            return Result.Failure(Failures.EmailAlreadyTaken);
        }
        
        var passHash = _cryptoService.Encrypt(request.Password);
        var newUser = User.Create(request.Username, passHash, request.FirstName, request.LastName,
            request.EmailAddress);

        await _usersRepository.AddAsync(newUser, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}