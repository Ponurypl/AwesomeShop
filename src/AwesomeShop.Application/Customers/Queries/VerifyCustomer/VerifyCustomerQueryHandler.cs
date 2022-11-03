using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.VerifyCustomer;

public sealed class VerifyCustomerQueryHandler : IQueryHandler<VerifyCustomerQuery, VerifyCustomerDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly ICryptoService _cryptoService;

    public VerifyCustomerQueryHandler(IUsersRepository usersRepository, IMapper mapper, ICryptoService cryptoService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _cryptoService = cryptoService;
    }

    public async Task<Result<VerifyCustomerDto>> Handle(VerifyCustomerQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByUsernameAsync(request.Username, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<VerifyCustomerDto>(Failures.NotExistingUser);
        }

        if (_cryptoService.Decrypt(user.PasswordHash) != request.Password)
        {
            return Result.Failure<VerifyCustomerDto>(Failures.NotExistingUser);
        }

        return Result.Success(_mapper.Map<VerifyCustomerDto>(user));
    }
}