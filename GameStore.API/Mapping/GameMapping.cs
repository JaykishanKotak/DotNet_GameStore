using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping
{
        // It will use as an extenation method so its static class
    public static class GameMapping
    {
        public static Game ToEntity(this CreateGameDto game)
        {
           return new Game() {
               Name = game.Name,
               GenreId = game.GenreId,
               Price = game.Price,
               ReleaseDate = game.ReleaseDate
           };
        }

        // To map game entity back to game dto
        public static GameDto toDto(this Game game)
        {
           return new(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );
        }
    }
}
