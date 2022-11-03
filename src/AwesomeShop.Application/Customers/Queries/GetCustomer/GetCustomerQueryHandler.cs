using OnboardingIntegrationExample.AwesomeShop.Application.Common.Cryptography;
using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public sealed class GetUserQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;
    private readonly ICryptoService _cryptoService;

    public GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper, ICryptoService cryptoService)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
        _cryptoService = cryptoService;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByUsernameAsync(request.Username, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure<CustomerDto>(Failures.NotExistingUser);
        }

        if (_cryptoService.Decrypt(user.PasswordHash) != request.Password)
        {
            return Result.Failure<CustomerDto>(Failures.NotExistingUser);
        }

        return _mapper.Map<CustomerDto>(user);
    }
}