namespace OnboardingIntegrationExample.AwesomeShop.Api.Client;

public partial class ApiClient
{
    private string? _token;

    public void SetToken(string? token)
    {
        _token = token;
    }

    partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
    {
        if (!string.IsNullOrWhiteSpace(_token))
        {
            request.Headers.Add("Authorization", $"Bearer {_token}");
        }
    }


}