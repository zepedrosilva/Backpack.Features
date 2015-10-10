using System.Diagnostics;
using System.Linq;

namespace Backpack.Features
{
    [DebuggerDisplay("Feature: {_feature.Name} ({GetType()}), Configuration: {IsEnabled()}, Feature: {_feature.IsEnabled()}")]
    internal class ConfigurationDrivenFeatureAdapter : IFeature
    {
        private readonly IFeature _feature;
        private readonly FeatureConfiguration _configuration;

        public ConfigurationDrivenFeatureAdapter(IFeature feature, FeatureConfiguration configuration = null)
        {
            Require.Argument.NotNull(feature);

            _feature = feature;
            _configuration = configuration ?? FeatureConfiguration.Current;
        }

        public string Name
        {
            get { return _feature.Name; }
        }

        public IFeature Feature 
        {
            get { return _feature; }
        }

        public bool IsEnabled()
        {
            if (_configuration != null)
            {
                var feature = _configuration.Features.SingleOrDefault(f => f.Name == _feature.Name);

                if (feature != null)
                {
                    return feature.IsEnabled;
                }
            }

            return _feature.IsEnabled();
        }
    }
}