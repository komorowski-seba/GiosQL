using Microsoft.Extensions.Configuration;

namespace Application.Extensions;

public static class ElasticsearchSettingsExtension
{
    public static string GetSettingsElasticsearchIndex(this IConfiguration configuration)
        => configuration.GetValue<string>("Elasticsearch:Index");

    public static string GetSettingsElasticsearchUrl(this IConfiguration configuration)
        => configuration.GetValue<string>("Elasticsearch:Url");
}