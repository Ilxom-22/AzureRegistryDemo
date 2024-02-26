var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "That worked!");

app.Run();