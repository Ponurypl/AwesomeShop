namespace OnboardingIntegrationExample.AwesomeShop.Domain.ValueTypes;

public sealed record RecipientDetails
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string AddressLine1 { get; private set; } = default!;
    public string? AddressLine2 { get; private set; }
    public string City { get; private set; } = default!;
    public string ZipCode { get; private set; } = default!;

    private RecipientDetails(string firstName, string lastName, string addressLine1, string? addressLine2, string city,
                             string zipCode)
    {
        FirstName = firstName;
        LastName = lastName;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        City = city;
        ZipCode = zipCode;
    }

    public static RecipientDetails Create(string firstName, string lastName, string addressLine1, string? addressLine2,
                                          string city, string zipCode)
    {
        return new RecipientDetails(firstName, lastName, addressLine1, addressLine2, city, zipCode);
    }
}