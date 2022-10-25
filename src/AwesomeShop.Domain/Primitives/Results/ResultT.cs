﻿namespace OnboardingIntegrationExample.AwesomeShop.Domain.Primitives.Results;

public sealed class Result<TValue> : Result
{
    private readonly TValue _value;

    internal Result(bool isSuccess, TValue value, Error? error = null) : base(isSuccess, error)
    {
        _value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);
}