// must match namespace of the folder with file system
namespace GameStore.Api.Dtos;

// records are immutable by default
// properties in records are init-only by default
public record class GenreDto(
    int Id,
    string Name
);
