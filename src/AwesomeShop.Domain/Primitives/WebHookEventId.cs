using System;

namespace OnboardingIntegrationExample.AwesomeShop.Domain.Primitives;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid,
                 converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct WebHookEventId
{

}