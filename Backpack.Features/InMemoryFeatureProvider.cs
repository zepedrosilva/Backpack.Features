using System;
using System.Collections.Generic;

namespace Backpack.Features
{
    public class InMemoryFeatureProvider : IFeatureProvider
    {
        private readonly Dictionary<Type, IFeature> _features;

        public InMemoryFeatureProvider(IEnumerable<IFeature> features)
        {
            Require.Argument.NotNull(features);

            _features = new Dictionary<Type, IFeature>();

            foreach (var feature in features)
            {
                _features.Add(feature.GetType(), feature);
            }
        }

        public IEnumerable<IFeature> GetAllFeatures()
        {
            return _features.Values;
        }

        public IFeature GetFeature<TFeature>() where TFeature : class, IFeature
        {
            return _features[typeof(TFeature)];
        }
    }
}