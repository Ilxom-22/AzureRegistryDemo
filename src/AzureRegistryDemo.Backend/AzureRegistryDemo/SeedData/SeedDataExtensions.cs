using AzureRegistryDemo.DataContexts;
using AzureRegistryDemo.Entities;
using Bogus;

namespace AzureRegistryDemo.SeedData;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

        if (!appDbContext.Users.Any())
            await appDbContext.SeedUsersAsync();
    }

    private static async ValueTask SeedUsersAsync(this AppDbContext appDbContext)
    {
        var usersFaker = new Faker<User>()
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email)
            .RuleFor(user => user.Password, data => data.Internet.Password());

        await appDbContext.AddRangeAsync(usersFaker.Generate(1));
        await appDbContext.SaveChangesAsync();
    }
}