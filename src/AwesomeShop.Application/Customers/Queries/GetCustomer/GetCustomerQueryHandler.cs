using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public class GetUserQueryHandler : IQueryHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<Result<CustomerDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByUsernameAsync(request.username, cancellationToken);

        if (user is null)
        {
            return Result.Failure<CustomerDto>(Failures.NotExistingUser);
        }

        return _mapper.Map<CustomerDto>(user);
    }
}