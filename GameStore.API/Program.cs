//// This line creates a builder object to help set up the web server.
// 'args' lets you pass in command line arguments if needed.
using GameStore.Api.Endpoints;
using GameStore.API.Dtos;

var builder = WebApplication.CreateBuilder(args);

// This line builds the web application using the settings from the builder.
// Think of this as preparing the server to start listening for requests.
var app = builder.Build();


// Call Games releted logic and endpoints
app.MapGamesEndpoints();

// This line sets up a simple route for the home page ('/').
// When someone visits the root URL, it will respond with "Hello World!".
app.MapGet("/", () => "Hello World!");

// This line actually starts the web server so it can handle requests.
// The app will keep running until you stop it.
app.Run();
