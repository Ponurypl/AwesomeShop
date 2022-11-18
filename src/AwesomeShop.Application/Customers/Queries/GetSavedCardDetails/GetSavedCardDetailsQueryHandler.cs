using OnboardingIntegrationExample.AwesomeShop.Application.Common.Persistence.Repositories;
using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Services;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetSavedCardDetails;

public sealed class GetSavedCardDetailsQueryHandler : IQueryHandler<GetSavedCardDetailsQuery, List<SavedCardDetailsDto>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPaymentApiService _paymentApiService;

    public GetSavedCardDetailsQueryHandler(IUsersRepository usersRepository, IPaymentApiService paymentApiService)
    {
        _usersRepository = usersRepository;
        _paymentApiService = paymentApiService;
    }

    public async Task<Result<List<SavedCardDetailsDto>>> Handle(GetSavedCardDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(new UserId(request.CustomerId), cancellationToken);
        if (user is null)
        {
            return Result.Failure<List<SavedCardDetailsDto>>(Failures.NotExistingUser);
        }

        List<SavedCardDetailsDto> results = new();
        foreach (var cardAlias in user.CardAliases)
        {
             var details = await _paymentApiService.GetTokenInfoAsync(cardAlias.TokenId);
             results.Add(new SavedCardDetailsDto()
                         {
                             CardNumber = details.CardNumber,
                             ExpiryDate = $"{details.ExpiryDate[..2]}/{details.ExpiryDate[2..]}",
                             Id = cardAlias.Id,
                         });
        }

        return Result.Success(results);
    }
}