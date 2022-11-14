namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;

public sealed record CartCheckoutCommand(Guid UserId, string FirstName, string LastName, string AddressLine1, 
                                         string? AddressLine2, string City, string ZipCode, string PhoneNumber, 
                                         PaymentMethods PaymentMethod, CardDetails? CardDetails) : ICommand<OrderDto>;