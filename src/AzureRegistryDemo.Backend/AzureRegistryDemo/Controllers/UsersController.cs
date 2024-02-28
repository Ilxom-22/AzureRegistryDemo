using AzureRegistryDemo.DataContexts;
using AzureRegistryDemo.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AzureRegistryDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(AppDbContext appDbContext) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        //return Ok(appDbContext.Users.ToList());
        return Ok(Environment.GetEnvironmentVariable("DbConnectionString"));
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateUser(User user)
    {
        await appDbContext.AddAsync(user);
        await appDbContext.SaveChangesAsync();

        return Ok(Environment.GetEnvironmentVariable("DbConnectionString"));
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser(User user)
    {
        appDbContext.Users.Update(user);
        await appDbContext.SaveChangesAsync();

        return Ok(Environment.GetEnvironmentVariable("DbConnectionString"));
    }

    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteUser(Guid userId)
    {
        var foundUser = appDbContext.Users.First(user => user.Id == userId);

        appDbContext.Users.Remove(foundUser);
        await appDbContext.SaveChangesAsync();

        return Ok(Environment.GetEnvironmentVariable("DbConnectionString"));
    }
}