using KawasakeChat.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();

// Middlewares
app.UseAuthentication();
app.UseAuthorization();

app.Run();
