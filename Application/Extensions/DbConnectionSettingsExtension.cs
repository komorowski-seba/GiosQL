using Microsoft.Extensions.Configuration;

namespace Application.Extensions;

public static class DbConnectionSettingsExtension
{
    public static string GetSettingsDBConnection(this IConfiguration configuration)
        => configuration.GetConnectionString("DBConnection");

    public static string GetSettingsDbConnectionHangfireConnection(this IConfiguration configuration)
        => configuration.GetConnectionString("HangfireConnection");

    public static string GetSettingsDbConnectionMarten(this IConfiguration configuration)
        => configuration.GetConnectionString("Marten");
}