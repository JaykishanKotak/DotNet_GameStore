using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameStore.Api.Data
{
    public class GameStoreContextFactory : IDesignTimeDbContextFactory<GameStoreContext>
    {
        public GameStoreContext CreateDbContext(string[] args)
        {
            // Load configuration (same as your Program.cs)
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<GameStoreContext>();
            var connectionString = configuration.GetConnectionString("GameStore");

            optionsBuilder.UseSqlite(connectionString);

            return new GameStoreContext(optionsBuilder.Options);
        }
    }
}
