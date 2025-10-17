using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Data;

// To execute any pending migrations
// public static class DataExtensions
// {
//     public static void MigrateDb(this WebApplication app)
//     {
//         // Create a scope to access the data
//         using var scope = app.Services.CreateScope();
//         var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
//         dbContext.Database.Migrate();
//     }
// }


// User Async suffix always for Async tasks
public static class DataExtensions
{
    public static async Task MigrateDbAsync(this WebApplication app)
    {
        // Create a scope to access the data
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
        await dbContext.Database.MigrateAsync();
    }
}
