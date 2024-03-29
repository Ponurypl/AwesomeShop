﻿namespace OnboardingIntegrationExample.AwesomeShop.Application.Customers.Queries.GetCustomer;

public sealed record CustomerDto
{
    public string Username { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string EmailAddress { get; set; } = default!;
}