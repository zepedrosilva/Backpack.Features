using System.Collections.Generic;

namespace Backpack.Features
{
    public interface IFeatureContainer
    {
        IEnumerable<IFeature> GetAllFeatures();
        IFeatureStateProvider OfType<TFeature>() where TFeature : class, IFeature;
    }
}