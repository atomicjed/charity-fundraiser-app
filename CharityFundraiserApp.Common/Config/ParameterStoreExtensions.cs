

using Amazon.Extensions.NETCore.Setup;
using Microsoft.Extensions.Configuration;

namespace CharityFundraiserApp.Common.Config;

public static class ParameterStoreExtensions
{
    public static IConfigurationBuilder AddCharityFundraiserParameterStore(this IConfigurationBuilder config)
    {
        config.AddSystemsManager(
            systemsConfig =>
            {
                systemsConfig.Path = Environment.GetEnvironmentVariable("SsmParameterStoreRoot");
                systemsConfig.ReloadAfter = null;
                systemsConfig.AwsOptions = new AWSOptions
                {
                    Profile = "sfs-dev",
                    Region = Amazon.RegionEndpoint.EUWest2
                };
            });
        return config;
    }
}