// Import the namespaces (like including libraries in other languages)
using GameStore.Api.Entities;         // This gives access to Game and Genre classes (your data models)
using Microsoft.EntityFrameworkCore;  // This gives access to Entity Framework Core features (database handling)

// This defines a "namespace" (like a folder or package name for organizing code)
namespace GameStore.Api.Data
{
    // This class represents your "Database Context"
    // A DbContext in Entity Framework is the main class that connects your C# code to a database.
    // Think of it as a bridge between your database tables and your C# objects.

    // The (DbContextOptions<GameStoreContext> options) part is used to configure how this context connects to the database.
    // The ": DbContext(options)" calls the base constructor from Entity Framework.
    public class GameStoreContext : DbContext
    {
        // Constructor - this runs when you create a new GameStoreContext object.
        // It passes configuration options (like database connection info) to the base class (DbContext).
        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        {
        }

        // These properties represent tables in your database.

        // This means there will be a "Games" table in the database
        // Each record (row) in the table corresponds to a "Game" object in your C# code.
        public DbSet<Game> Games => Set<Game>();

        // Same for "Genres" — this represents a "Genres" table in the database.
        public DbSet<Genre> Genres => Set<Genre>();

        // Method to add Static Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Genre>().HasData(
             new { Id = 1, Name = "Fighting" },
             new { Id = 2, Name = "Action" },
             new { Id = 3, Name = "Adventure" },
             new { Id = 4, Name = "Role-Playing" },
             new { Id = 5, Name = "Simulation" },
             new { Id = 6, Name = "Strategy" },
             new { Id = 7, Name = "Sports" },
             new { Id = 8, Name = "Racing" },
             new { Id = 9, Name = "Puzzle" },
             new { Id = 10, Name = "Platformer" },
             new { Id = 11, Name = "Shooter" },
             new { Id = 12, Name = "Stealth" },
             new { Id = 13, Name = "Survival" },
             new { Id = 14, Name = "Horror" },
             new { Id = 15, Name = "MMORPG" },
             new { Id = 16, Name = "Roguelike" },
             new { Id = 17, Name = "Sandbox" },
             new { Id = 18, Name = "Idle" },
             new { Id = 19, Name = "Rhythm" },
             new { Id = 20, Name = "Party" }
            );

        }
    }
}
