using System;
using System.Linq;
using Moq;
using Should;
using Should.Fluent;

namespace Backpack.Features.Tests
{
    public class FeatureContainerTests
    {
        public void Ctor_WithNullProvider_ThrowsArgumentNullException()
        {
            Action action = () => new FeatureContainer((IFeatureProvider) null);

            action.ShouldThrow<ArgumentNullException>();
        }

        public void GetAllFeatures_WithProvider_FeaturesAreReturned()
        {
            var features = new[] { new TestFeature(), new TestFeature2(), new TestFeature3() };

            var provider = new Mock<IFeatureProvider>();
            provider.Setup(p => p.GetAllFeatures()).Returns(features);

            var container = new FeatureContainer(provider.Object);

            var featureSet = container.GetAllFeatures()
                .Select(f => (f as ConfigurationDrivenFeatureAdapter).Feature).ToList();

            featureSet.Should().Count.Exactly(features.Length);

            foreach (var feature in features)
            {
                featureSet.Should().Contain.Item(feature);
            }
        }

        public void OfType_WithProvider_FeatureIsReturned()
        {
            var feature = new TestFeature();

            var provider = new Mock<IFeatureProvider>();
            provider.Setup(p => p.GetFeature<TestFeature>()).Returns(feature);

            var container = new FeatureContainer(provider.Object);

            var output = container.OfType<TestFeature>();

            (output as ConfigurationDrivenFeatureAdapter).Feature.Should().Equal(feature);
        }
    }
}