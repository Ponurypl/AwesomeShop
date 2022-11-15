using Microsoft.Extensions.Options;
using OnboardingIntegrationExample.AwesomeShop.Application.Orders.Commands.CartCheckout;
using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Models;
using OnboardingIntegrationExample.AwesomeShop.Application.Payments.Services;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;
using OnboardingIntegrationExample.AwesomeShop.Infrastructure.Configuration.WorldLine;
using OnlinePayments.Sdk;
using OnlinePayments.Sdk.Domain;

namespace OnboardingIntegrationExample.AwesomeShop.Infrastructure.Payment;

public sealed class WorldLineApiService : IPaymentApiService
{
    private const string PSPID = "AwesomeShop1";

    private readonly WorldLineApiConfigurationDetails _configuration;
    private readonly Uri _apiUri;

    public WorldLineApiService(IOptionsMonitor<WorldLineApiConfigurationDetails> configuration)
    {
        _configuration = configuration.CurrentValue;
        _apiUri = new Uri(_configuration.Address);
    }

    public async Task<PaymentDetails> PayByCard(UserId customerId, double paymentAmount, CardDetails cardDetails)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");
        
        var card = new Card()
                   {
                       CardNumber = cardDetails.CardNumber.Replace(" ",""),
                       Cvv = cardDetails.CVV,
                       CardholderName = cardDetails.HolderName,
                       ExpiryDate = cardDetails.ExpirationDate.Replace("/", "")
                   };

        int productId = 1;
        if (card.CardNumber[0] == '5')
        {
            productId = 3;
        }

        CreatePaymentRequest request = new CreatePaymentRequest()
                                       {
                                           Order = new Order()
                                                   {
                                                       AmountOfMoney = new AmountOfMoney()
                                                                       {
                                                                           CurrencyCode = "EUR",
                                                                           Amount = Convert.ToInt64(paymentAmount * 100)
                                                                       },
                                                       Customer = new Customer()
                                                                  {
                                                                      MerchantCustomerId = customerId.Value.ToString()
                                                                  },
                                                       References = new OrderReferences() {}
                                                   },
                                           CardPaymentMethodSpecificInput = new CardPaymentMethodSpecificInput()
                                                                            {
                                                                                AuthorizationMode = AuthorizationMode.Sale,
                                                                                Card = card,
                                                                                PaymentProductId = productId
                                                                            }
                                       };

        var createPaymentResponse = await client.WithNewMerchant(PSPID).Payments.CreatePayment(request);

        if (createPaymentResponse is null)
        {
            throw new NullReferenceException();
        }

        return new PaymentDetails()
               {
                   PaymentId = createPaymentResponse.Payment.Id.Split("_")[0],
                   StatusCode = createPaymentResponse.Payment.StatusOutput.StatusCode!.Value
               };

    }
}