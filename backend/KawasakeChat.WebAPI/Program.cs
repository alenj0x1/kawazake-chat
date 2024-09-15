using KawasakeChat.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();
app.Run();
