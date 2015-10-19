using System.Configuration;
using Should.Fluent;

namespace Backpack.Features.Tests
{
    public class FeatureConfigurationTests
    {
        public void Current_LoadsCorrectConfigurationSection()
        {
            var configurationSection = ConfigurationManager.GetSection("backpack.features") as FeatureConfiguration;
            var configuration = FeatureConfiguration.Current;

            configuration.Should().Equal(configurationSection);
        }
    }
}