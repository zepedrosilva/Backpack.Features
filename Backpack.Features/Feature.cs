using System.Diagnostics;

namespace Backpack.Features
{
    [DebuggerDisplay("Feature: {Name} ({GetType()}), Enabled: {IsEnabled()}")]
    public abstract class Feature : IFeature
    {
        public abstract string Name { get; }

        public abstract bool IsEnabled();
    }

    [DebuggerDisplay("Feature: {Name} ({GetType()}), Enabled: {IsEnabled()}")]
    public abstract class Feature<TStateProvider> : Feature where TStateProvider : class, IFeatureStateProvider, new()
    {
        private readonly IFeatureStateProvider _provider;

        protected Feature()
        {
            _provider = new TStateProvider();
        }

        public override bool IsEnabled()
        {
            return _provider.IsEnabled();
        }
    }
}