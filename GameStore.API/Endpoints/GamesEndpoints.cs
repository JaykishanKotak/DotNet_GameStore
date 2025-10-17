
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;


public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDto> games = [
        new (1, "Batman: Arkham Asylum", "Action", 9.99M, new DateOnly(2009, 8, 25)),
        new (2, "The Witcher 3: Wild Hunt", "RPG", 29.99M, new DateOnly(2015, 5, 19)),
        new (3, "Minecraft", "Sandbox", 19.99M, new DateOnly(2011, 11, 18)),
        new (4, "Elden Ring", "Action RPG", 59.99M, new DateOnly(2022, 2, 25)),
        new (5, "Red Dead Redemption 2", "Action-Adventure", 49.99M, new DateOnly(2018, 10, 26)),
        new (6, "Cyberpunk 2077", "RPG", 39.99M, new DateOnly(2020, 12, 10)),
        new (7, "God of War", "Action", 39.99M, new DateOnly(2018, 4, 20)),
        new (8, "Hollow Knight", "Metroidvania", 14.99M, new DateOnly(2017, 2, 24)),
        new (9, "Stardew Valley", "Simulation", 14.99M, new DateOnly(2016, 2, 26)),
        new (10, "The Legend of Zelda: Breath of the Wild", "Action-Adventure", 59.99M, new DateOnly(2017, 3, 3))
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        // Group to denote common route "/games"
        var group = app.MapGroup("games").WithParameterValidation();

        // GET /games
        //app.MapGet("games", () => games);
        //group.MapGet("/", () => games);
        group.MapGet("/", (GameStoreContext dbContext) => 
            dbContext.Games.Include(game => game.Genre).Select(game => game.ToGameSummaryDto()).AsNoTracking()
        );

        // GET /games/1
        group.MapGet("/{id}", (int Id, GameStoreContext dbContext) =>
        {
            // GameSummaryDto? game = games.Find(game => game.Id == Id);
            Game? game = dbContext.Games.Find(Id); 

            return game is null ? Results.NotFound() : Results.Ok(game.ToGameDetailsDto());

        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => {

            // Validations
            //if (string.IsNullOrEmpty(newGame.Name))
            //{
            //    return Results.BadRequest("Name is required !");
            //}

            // Using mappings
            Game game = newGame.ToEntity();

            // Entity Framework will auto populate it
            // game.Genre = dbContext.Genres.Find(newGame.GenreId);

            //Game game = new()
            //{
            //    Name = newGame.Name,
            //    Genre = dbContext.Genres.Find(newGame.GenreId),
            //    GenreId = newGame.GenreId,
            //    Price = newGame.Price,
            //    ReleaseDate = newGame.ReleaseDate,
            //};

            //GameSummaryDto game = new(
            //    games.Count + 1,
            //    newGame.Name,
            //    newGame.Genre,
            //    newGame.Price,
            //    newGame.ReleaseDate
            //);

            //games.Add(game);
            dbContext.Games.Add(game);

            // Transform statement and execute operation in db
            dbContext.SaveChanges();

            // DTO out of game entity to return back details to client
            //GameSummaryDto gameDto = new(
            //    game.Id,
            //    game.Name,
            //    game.Genre!.Name,
            //    game.Price,
            //    game.ReleaseDate
            //);

            // Result class contains multiple pre build resources
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
        }).WithParameterValidation();

        // PUT /games/id
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
        {
            // var index = games.FindIndex(game => game.Id == id);

            var existingGame = dbContext.Games.Find(id);

            // Alternativly We can create resource as well
            // if(Index == -1)
            if (existingGame is null)
            {
                return Results.NotFound();
            }

            // games[index] = new GameSummaryDto(
            //     id,
            //     updatedGame.Name,
            //     updatedGame.GenreId,
            //     updatedGame.Price,
            //     updatedGame.ReleaseDate
            // );

            // Entry method is used to locate current entity inside the game
            dbContext.Entry(existingGame).CurrentValues.SetValues(updatedGame.ToEntity(id));

            // Save Changes to DB
            dbContext.SaveChanges();
            return Results.NoContent();
        });

        // DELETE /games/id
        group.MapDelete("/{id}", (int id, GameStoreContext dbContext) =>
        {
            // Find Game To Remove
            // ExecuteDelete will directly delete item from db
            dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
            //games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });



        return group;

    }
}