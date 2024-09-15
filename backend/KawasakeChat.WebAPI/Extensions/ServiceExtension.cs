using KawasakeChat.Domain.Context;

namespace KawasakeChat.WebAPI.Extensions;

public static class ServiceExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddNpgsql<KawasakeChatDbContext>(configuration.GetConnectionString("Database") ?? throw new Exception("Missing 'Database' connection string"));
    }
}