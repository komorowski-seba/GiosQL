using Microsoft.Extensions.Configuration;

namespace Application.Extensions;

public static class GiosSettingsExtension
{
    public static string GetSettingsGiosStationsUrl(this IConfiguration configuration)
        => configuration.GetValue<string>("Gios:StationsUrl");

    public static string GetSettingsGiosQualityUrl(this IConfiguration configuration)
        => configuration.GetValue<string>("Gios:QualityUrl");
}