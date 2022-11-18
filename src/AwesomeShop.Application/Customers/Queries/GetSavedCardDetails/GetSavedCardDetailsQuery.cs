namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetSavedCardDetails;

public sealed record GetSavedCardDetailsQuery(Guid CustomerId) : IQuery<List<SavedCardDetailsDto>>;