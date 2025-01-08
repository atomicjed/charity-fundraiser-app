using Npgsql;

namespace CharityFundraiserApp.ApplicationDatabase.Config;
using Newtonsoft.Json;

public class ApplicationDatabaseSettings
{
    public const string SectionName = nameof(ApplicationDatabaseSettings);

    public string DbHostName { get; set; }
    public string DbName { get; set; }
    public string DbPort { get; set; }

    [JsonProperty("username")] public string Username { get; set; }

    [JsonProperty("password")] public string Password { get; set; }

    public string CreateConnectionString()
    {
        var builder = new NpgsqlConnectionStringBuilder();
        builder["Server"] = DbHostName;
        builder["Username"] = Username;
        builder["Database"] = DbName;
        builder["Port"] = DbPort;
        builder["Password"] = Password;
        builder["SSLMode"] = "Require";
        builder["TrustServerCertificate"] = true;
        return builder.ConnectionString;
    }
}