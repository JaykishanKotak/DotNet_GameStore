
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;

namespace GameStore.Api.Endpoints;


public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameDto> games = [
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
        group.MapGet("/", () => games);

        // GET /games/1
        group.MapGet("/{id}", (int Id) =>
        {
            GameDto? game = games.Find(game => game.Id == Id);

            return game is null ? Results.NotFound() : Results.Ok(game);

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
            game.Genre = dbContext.Genres.Find(newGame.GenreId);

            //Game game = new()
            //{
            //    Name = newGame.Name,
            //    Genre = dbContext.Genres.Find(newGame.GenreId),
            //    GenreId = newGame.GenreId,
            //    Price = newGame.Price,
            //    ReleaseDate = newGame.ReleaseDate,
            //};

            //GameDto game = new(
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
            //GameDto gameDto = new(
            //    game.Id,
            //    game.Name,
            //    game.Genre!.Name,
            //    game.Price,
            //    game.ReleaseDate
            //);

            // Result class contains multiple pre build resources
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game.toDto());
        }).WithParameterValidation();

        // PUT /games/id
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            // Alternativly We can create resource as well
            if (index == -1)
            {
                return Results.NotFound();
            }

            games[index] = new GameDto(
                id,
                updatedGame.Name,
                updatedGame.Genre,
                updatedGame.Price,
                updatedGame.ReleaseDate
            );

            return Results.NoContent();
        });

        // DELETE /games/id
        group.MapDelete("/{id}", (int id) =>
        {
            games.RemoveAll(game => game.Id == id);

            return Results.NoContent();
        });



        return group;

    }
}