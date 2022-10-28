﻿namespace OnboardingIntegrationExample.AwesomeShop.Application.Orders;

public static class Failures
{
    public static Error InvalidUsername => new(nameof(InvalidUsername), "Invalid customer username");
    public static Error InvalidProduct => new(nameof(InvalidProduct), "Invalid product Id");
    public static Error InvalidQuantity => new(nameof(InvalidQuantity), "Invalid quantity");

    public static Error NoOpenCart =>
        new(nameof(NoOpenCart), "Given user doesn't have open cart");
}