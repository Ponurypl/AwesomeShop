namespace OnboardingIntegrationExample.AwesomeShop.Api.V1.Categories.GetCategories;


public sealed record GetCategoriesResponse(List<GetCategoriesResponse.Category> Categories)
{
    public sealed record Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}