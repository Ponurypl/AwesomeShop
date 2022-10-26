using FluentValidation;
using OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;
using OnboardingIntegrationExample.AwesomeShop.Domain.Validators;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public class User
{
    public UserId Id { get; }
    public string Username { get; } 
    public string PasswordHash { get; } 
    public string FirstName { get; } 
    public string LastName { get; }
    public string EmailAddress { get; } 

    private User(string username, string passwordHash, string firstName, string lastName, string emailAddress)
    {
        Id = UserId.New();
        Username = username;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
    }

    public static User Create(string username, string passwordHash, string firstName, string lastName,
        string emailAddress)
    {
        var user = new User(username, passwordHash, firstName, lastName, emailAddress);
        var validator = new UserValidator();
        var result = validator.Validate(user);
        
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        return user;
    }
}