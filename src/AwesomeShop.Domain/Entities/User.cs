﻿using OnboardingIntegrationExample.AwesomeShop.Domain.Abstractions;
using OnboardingIntegrationExample.AwesomeShop.Domain.ValueTypes;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Entities;

public sealed class User : Entity<UserId>
{
    public string Username { get; private set; } 
    public string PasswordHash { get; private set; } 
    public string FirstName { get; private set; } 
    public string LastName { get; private set; }
    public string EmailAddress { get; private set; }
    public List<CardAlias> CardAliases { get; } = new();

    private User(UserId id, string username, string passwordHash, string firstName, string lastName, string emailAddress)
        : base(id)
    {
        Username = username;
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        EmailAddress = emailAddress;
    }

    public static User Create(string username, string passwordHash, string firstName, string lastName,
        string emailAddress)
    {
        var user = new User(UserId.New(), username, passwordHash, firstName, lastName, emailAddress);
        var validator = new UserValidator();
        var result = validator.Validate(user);
        
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }

        return user;
    }

    public void AddPaymentToken(string tokenId)
    {
        CardAliases.Add(new CardAlias { Id = Guid.NewGuid(), TokenId = tokenId });
    }
}