// must match namespace of the folder with file system
namespace GameStore.Api.Dtos;

// records are immutable by default
// properties in records are init-only by default
public record class GameDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
