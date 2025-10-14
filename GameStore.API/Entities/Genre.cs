namespace GameStore.Api.Entities;


public class Genre
{
    public  int Id { get; set; }
    //public string Name { get; set; } = string.Empty
    //public string? Name { get; set; }

    // As it is required, we need to pass the Value always when we constrtuct the object
    public required string Name { get; set; }

}