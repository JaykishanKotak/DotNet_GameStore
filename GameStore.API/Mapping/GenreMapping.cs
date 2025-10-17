using GameStore.Api.Dtos;
using GameStore.Api.Entities;

namespace GameStore.Api.Mapping
{
    // It will use as an extenation method so its static class
    public static class GenreMapping
    {
        public static GenreDto ToDto(this Genre genre)
        {
            return new GenreDto(genre.Id, genre.Name);
        }
    }
}