using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;
using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Models;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

namespace OnboardingIntegrationExample.AwesomeShop.Application.Payments.Services;

public interface IPaymentApiService
{
    Task<PaymentDetails> PayByCard(UserId customerId, double paymentAmount, CardDetails cardDetails);
}