using System;
using System.Collections.Generic;
using System.Linq;
using Should;
using Should.Fluent;

namespace Backpack.Features.Tests
{
    public class InMemoryFeatureProviderTests
    {
        public void Ctor_WithNullFeatureSet_ThrowsArgumentNullException()
        {
            Action action = () => new InMemoryFeatureProvider(null);

            action.ShouldThrow<ArgumentNullException>();
        }

        public void Ctor_WithEmptyFeatureSet_NoFeaturesAreAdded()
        {
            var provider = new InMemoryFeatureProvider(new IFeature[] { });

            provider.GetAllFeatures().Should().Be.Empty();
        }

        public void Ctor_WithValidFeatureSet_FeaturesAreAdded()
        {
            var features = new[] { new TestFeature(), new TestFeature2(), new TestFeature3() };
            var provider = new InMemoryFeatureProvider(features);

            var featureSet = provider.GetAllFeatures().ToList();

            featureSet.Should().Count.Exactly(features.Length);

            foreach (var feature in features)
            {
                featureSet.Should().Contain.Item(feature);
            }
        }

        public void Ctor_WithTwoFeaturesOfTheSameType_ThrowsArgumentException()
        {
            Action action = () => new InMemoryFeatureProvider(new[] {new TestFeature(), new TestFeature()});

            action.ShouldThrow<ArgumentException>();
        }

        public void GetFeatures_WithoutTheFeatureBeingRegistered_ThrowsKeyNotFoundException()
        {
            var provider = new InMemoryFeatureProvider(new IFeature[] { });
            Action action = () => provider.GetFeature<TestFeature>();

            action.ShouldThrow<KeyNotFoundException>();
        }

        public void GetFeatures_WhenTheFeatureBeingRegistered_TheFeatureIsReturned()
        {
            var featureToLookFor = new TestFeature2();
            var provider = new InMemoryFeatureProvider(new[] { new TestFeature(), featureToLookFor, new TestFeature3() });

            var feature = provider.GetFeature<TestFeature2>();

            feature.Should().Be.SameAs(featureToLookFor);
        }
    }
}