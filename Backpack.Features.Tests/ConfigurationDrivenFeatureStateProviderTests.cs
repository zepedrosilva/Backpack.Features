using System.Collections.Generic;
using Moq;
using Ploeh.AutoFixture;
using Should.Fluent;

namespace Backpack.Features.Tests
{
    public class ConfigurationDrivenFeatureStateProviderTests
    {
        public void IsEnabled_WhenFeatureIsDisabledInTheConfiguration_FeatureIsDisabled()
        {
            var fixture = new Fixture();
            var feature = fixture.Create<TestFeature>();
            feature.State = true;

            var configuration = GetConfiguration(feature, false);

            var configurationDrivenFeature = new ConfigurationDrivenFeatureAdapter(feature, configuration);

            configurationDrivenFeature.IsEnabled().Should().Be.False();
        }

        [Input(true)]
        [Input(false)]
        public void IsEnabled_WhenFeatureIsEnabledInTheConfiguration_OriginalFeatureStateIsReturned(bool featureState)
        {
            var fixture = new Fixture();
            var feature = fixture.Create<TestFeature>();
            feature.State = featureState;

            var configuration = GetConfiguration(feature, featureState);

            var configurationDrivenFeature = new ConfigurationDrivenFeatureAdapter(feature, configuration);

            configurationDrivenFeature.IsEnabled().Should().Equal(featureState);
        }

        private FeatureConfiguration GetConfiguration(IFeature feature, bool featureConfigurationIsEnabled)
        {
            var featureElement = new FeatureElement { Name = feature.Name, IsEnabled = featureConfigurationIsEnabled };

            var featureSet = new Mock<FeatureElementsCollection>();
            featureSet.Setup(s => s.GetEnumerator()).Returns(new List<FeatureElement> { featureElement }.GetEnumerator());

            return new FeatureConfiguration { Features = featureSet.Object };
        }
    }
}