using System.Collections.Generic;

namespace Backpack.Features
{
    public interface IFeatureProvider
    {
        IEnumerable<IFeature> GetAllFeatures();
        IFeature GetFeature<TFeature>() where TFeature : class, IFeature;
    }
}