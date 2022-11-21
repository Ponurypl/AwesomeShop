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
                                    PaymentProductId = productId,
                                    Tokenize = cardDetails.SaveCardInfo
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

        string? tokenId = null;
        if (createPaymentResponse.CreationOutput.TokenizationSucceeded is true &&
            createPaymentResponse.CreationOutput.IsNewToken is true)
        {
            tokenId = createPaymentResponse.CreationOutput.Token;
        }

        return new PaymentDetails()
               {
                   PaymentId = createPaymentResponse.Payment.Id.Split("_")[0],
                   RawPaymentId = createPaymentResponse.Payment.Id,
                   StatusCode = createPaymentResponse.Payment.StatusOutput.StatusCode!.Value,
                   TokenId = tokenId
               };
    }

    public async Task<PaymentDetails> SetupDelayedCardPaymentAsync(UserId customerId, double paymentAmount,
                                                                   CardDetails cardDetails)
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
                                    PaymentProductId = productId,
                                    Tokenize = cardDetails.SaveCardInfo
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

        string? tokenId = null;
        if (createPaymentResponse.CreationOutput.TokenizationSucceeded is true && 
            createPaymentResponse.CreationOutput.IsNewToken is true)
        {
            tokenId = createPaymentResponse.CreationOutput.Token;
        }

        return new PaymentDetails()
               {
                   PaymentId = createPaymentResponse.Payment.Id.Split("_")[0],
                   RawPaymentId = createPaymentResponse.Payment.Id,
                   StatusCode = createPaymentResponse.Payment.StatusOutput.StatusCode!.Value,
                   TokenId = tokenId
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

    public async Task<TokenDetails> GetTokenInfoAsync(string tokenId)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");

        var tokenResponse = await client.WithNewMerchant(PSPID).Tokens.GetToken(tokenId);

        return new TokenDetails()
               {
                   CardNumber = tokenResponse.Card.Data.CardWithoutCvv.CardNumber,
                   CardHolderName = tokenResponse.Card.Data.CardWithoutCvv.CardholderName,
                   ExpiryDate = tokenResponse.Card.Data.CardWithoutCvv.ExpiryDate,
                   TokenId = tokenResponse.Id
               };
    }

    public async Task<PaymentDetails> PayByTokenAsync(UserId customerId, double paymentAmount, string tokenId, string cvv)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");
        
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
            Token = tokenId,
            Card = new Card() { Cvv = cvv }
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

    public async Task<HostedCheckoutDetails> CreateHostedSalePaymentAsync(UserId customerId, double paymentAmount, string returnUrl)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");
        
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

        var hostedCheckoutRequest = new CreateHostedCheckoutRequest
                                    {
                                        Order = order,
                                        CardPaymentMethodSpecificInput = new CardPaymentMethodSpecificInputBase
                                                                         {
                                                                             AuthorizationMode = AuthorizationMode.Sale
                                                                         },
                                        HostedCheckoutSpecificInput = new HostedCheckoutSpecificInput()
                                                                      {
                                                                          ReturnUrl = returnUrl
                                                                      }
                                    };

        var createHostedCheckoutResponse = await client.WithNewMerchant(PSPID)
                                                       .HostedCheckout
                                                       .CreateHostedCheckout(hostedCheckoutRequest);

        return new HostedCheckoutDetails()
               {
                   Id = createHostedCheckoutResponse.HostedCheckoutId,
                   RedirectUrl = createHostedCheckoutResponse.RedirectUrl
               };
    }

    public async Task<HostedCheckoutDetails> CreateHostedAuthorizationPaymentAsync(UserId customerId, double paymentAmount, string returnUrl)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");

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

        var hostedCheckoutRequest = new CreateHostedCheckoutRequest
        {
            Order = order,
            CardPaymentMethodSpecificInput = new CardPaymentMethodSpecificInputBase
            {
                AuthorizationMode = AuthorizationMode.FinalAuthorization
            },
            HostedCheckoutSpecificInput = new HostedCheckoutSpecificInput()
            {
                ReturnUrl = returnUrl
            }
        };

        var createHostedCheckoutResponse = await client.WithNewMerchant(PSPID)
                                                       .HostedCheckout
                                                       .CreateHostedCheckout(hostedCheckoutRequest);

        return new HostedCheckoutDetails()
        {
            Id = createHostedCheckoutResponse.HostedCheckoutId,
            RedirectUrl = createHostedCheckoutResponse.RedirectUrl
        };
    }

    public async Task<PaymentDetails> GetHostedCheckoutStatusAsync(string hostedPaymentId)
    {
        var client = Factory.CreateClient(_configuration.ApiKey, _configuration.ApiSecret, _apiUri, "AwesomeShop");
        

        var hostedCheckout = await client.WithNewMerchant(PSPID)
                                                       .HostedCheckout
                                                       .GetHostedCheckout(hostedPaymentId);
        return new PaymentDetails()
        {
            PaymentId = hostedCheckout.CreatedPaymentOutput.Payment.Id.Split("_")[0],
            RawPaymentId = hostedCheckout.CreatedPaymentOutput.Payment.Id,
            StatusCode = hostedCheckout.CreatedPaymentOutput.Payment.StatusOutput.StatusCode!.Value
        };
    }

    private static Card GetCardObject(CardDetails cardDetails)
    {
        return new Card()
               {
                   CardNumber = cardDetails.CardNumber.Replace(" ", ""),
                   Cvv = cardDetails.CVV,
                   CardholderName = cardDetails.HolderName,
                   ExpiryDate = cardDetails.ExpirationDate.Replace("/", "")
               };
    }
}