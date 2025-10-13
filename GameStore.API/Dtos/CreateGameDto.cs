namespace GameStore.API.Dtos;

// Its only for Create Game, Send Game Data to server will have diff DTO
public record class CreateGameDto(
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
