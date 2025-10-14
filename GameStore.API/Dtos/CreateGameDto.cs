using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

// Its only for Create Game, Send Game Data to server will have diff DTO

// Data Annotations are attribures that we can apply on Properties
// Data Annotations not working in New Core MVC Versions, For that we need to use Endpoint Filters
public record class CreateGameDto(
    [Required] [StringLength(50)] string Name,
    [Required] [StringLength(20)] string Genre,
    [Range(1,100)] decimal Price,
    DateOnly ReleaseDate
);
