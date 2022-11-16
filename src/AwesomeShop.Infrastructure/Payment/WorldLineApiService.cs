using Azure.Core;
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

    public async Task<PaymentDetails> PayByCardAsync(UserId customerId, double paymentAmount, CardDetails cardDetails)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");
        
        var card = GetCardObject(cardDetails);

        var productId = 1;
        if (card.CardNumber[0] == '5')
        {
            productId = 3;
        }

        Order order = new()
                      {
                          AmountOfMoney = new AmountOfMoney()
                                          {
                                              CurrencyCode = "EUR",
                                              Amount = Convert.ToInt64(paymentAmount * 100)
                                          },
                          Customer = new Customer()
                                     {
                                         MerchantCustomerId = customerId.Value.ToString()
                                     }
                      };

        var cardPaymentMethod = new CardPaymentMethodSpecificInput()
                                {
                                    AuthorizationMode = AuthorizationMode.Sale,
                                    Card = card,
                                    PaymentProductId = productId
                                };

        CreatePaymentRequest request = new()
                                       {
                                           Order = order,
                                           CardPaymentMethodSpecificInput = cardPaymentMethod
                                       };

        var createPaymentResponse = await client.WithNewMerchant(PSPID).Payments.CreatePayment(request);

        if (createPaymentResponse is null)
        {
            throw new NullReferenceException();
        }

        return new PaymentDetails()
               {
                   PaymentId = createPaymentResponse.Payment.Id.Split("_")[0],
                   RawPaymentId = createPaymentResponse.Payment.Id,
                   StatusCode = createPaymentResponse.Payment.StatusOutput.StatusCode!.Value
               };

    }

    public async Task<PaymentDetails> SetupDelayedCardPaymentAsync(UserId customerId, double paymentAmount, CardDetails cardDetails)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");

        var card = GetCardObject(cardDetails);

        var productId = 1;
        if (card.CardNumber[0] == '5')
        {
            productId = 3;
        }

        Order order = new()
        {
            AmountOfMoney = new AmountOfMoney()
            {
                CurrencyCode = "EUR",
                Amount = Convert.ToInt64(paymentAmount * 100)
            },
            Customer = new Customer()
            {
                MerchantCustomerId = customerId.Value.ToString()
            }
        };

        var cardPaymentMethod = new CardPaymentMethodSpecificInput()
        {
            AuthorizationMode = AuthorizationMode.FinalAuthorization,
            Card = card,
            PaymentProductId = productId
        };

        CreatePaymentRequest request = new()
        {
            Order = order,
            CardPaymentMethodSpecificInput = cardPaymentMethod
        };

        var createPaymentResponse = await client.WithNewMerchant(PSPID).Payments.CreatePayment(request);

        if (createPaymentResponse is null)
        {
            throw new NullReferenceException();
        }

        return new PaymentDetails()
        {
            PaymentId = createPaymentResponse.Payment.Id.Split("_")[0],
            RawPaymentId = createPaymentResponse.Payment.Id,
            StatusCode = createPaymentResponse.Payment.StatusOutput.StatusCode!.Value
        };
    }

    public async Task<PaymentDetails> CaptureDelayedCardPaymentAsync(string paymentId, double paymentAmount)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");

        CapturePaymentRequest request = new()
                                        {
                                            Amount = Convert.ToInt64(paymentAmount * 100), 
                                            IsFinal = true
                                        };

        var capturePaymentResponse = await client.WithNewMerchant(PSPID).Payments.CapturePayment(paymentId, request);

        return new PaymentDetails()
               {
                   PaymentId = paymentId,
                   StatusCode = capturePaymentResponse.StatusOutput.StatusCode!.Value
               };
    }

    private static Card GetCardObject(CardDetails cardDetails)
    {
        return new Card()
               {
                   CardNumber = cardDetails.CardNumber.Replace(" ",""),
                   Cvv = cardDetails.CVV,
                   CardholderName = cardDetails.HolderName,
                   ExpiryDate = cardDetails.ExpirationDate.Replace("/", "")
               };
    }
}