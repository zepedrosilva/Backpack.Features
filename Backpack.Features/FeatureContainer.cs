using System.Collections.Generic;

namespace Backpack.Features
{
    public class FeatureContainer : IFeatureContainer
    {
        private readonly IFeatureProvider _provider;

        public FeatureContainer(IEnumerable<IFeature> features)
        {
            Require.Argument.NotNull(features);

            _provider = new InMemoryFeatureProvider(features);
        }

        public FeatureContainer(IFeatureProvider provider)
        {
            Require.Argument.NotNull(provider);

            _provider = provider;
        }

        public IEnumerable<IFeature> GetAllFeatures()
        {
            var features = _provider.GetAllFeatures();

            foreach (var feature in features)
            {
                yield return new ConfigurationDrivenFeatureAdapter(feature);
            }
        }

        public IFeatureStateProvider OfType<TFeature>() where TFeature : class, IFeature
        {
            var feature = _provider.GetFeature<TFeature>();
            return new ConfigurationDrivenFeatureAdapter(feature);
        }
    }
}