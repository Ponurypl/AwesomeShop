﻿namespace OnboardingIntegrationExample.AwesomeShop.WebIU.Models;

public class OrderHeader
{
    public Guid Id { get; set; }
    public string Number { get; set; } = default!;
    public string Status { get; set; } = default!;
    public double Summary { get; set; }
    public DateTimeOffset CreationDate { get; set; }
}