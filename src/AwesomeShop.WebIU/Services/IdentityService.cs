namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Services;

public class IdentityService
{
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;

    public string Username { get; private set; } = default!;
    public string EmailAddress { get; private set; } = default!;

    public string? Token { get; private set; }
    public DateTimeOffset? ExpireAt { get; private set; }

    public bool IsSignedIn()
    {
        return DateTime.UtcNow < ExpireAt;
    }

    public bool RequireSetup()
    {
        return IsSignedIn() && string.IsNullOrWhiteSpace(Username);
    }

    public void SignIn(string token, DateTimeOffset expireAt)
    {
        Token = token;
        ExpireAt = expireAt;
    }

    public void SetupIdentity(string firstName, string lastname, string username, string emailAddress)
    {
        FirstName = firstName;
        LastName = lastname;
        Username = username;
        EmailAddress = emailAddress;
    }

    public void SignOut()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Username = string.Empty;
        EmailAddress = string.Empty;
        ExpireAt = null;
        Token = null;
    }

    public string Tokenize()
    {
        var token = $"{FirstName}_{LastName}_{Username}_{EmailAddress}_{Token}_{ExpireAt:O}";
        return token;
    }

    public void Restore(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return;
        }

        var parts = token.Split("_");
        FirstName = parts[0];
        LastName = parts[1];
        Username = parts[2];
        EmailAddress = parts[3];
        Token = parts[4];
        ExpireAt = DateTimeOffset.Parse(parts[5]);
    }
}