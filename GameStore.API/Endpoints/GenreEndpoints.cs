
using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Endpoints;

public static class GenreEndpoints
{
    public static RouteGroupBuilder MapGenresEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("Genres");

        group.Map("/", async (GameStoreContext dbContext) => await dbContext.Genres.Select(genre => genre.ToDto()).AsNoTracking().ToListAsync());

        return group;
    }
}