namespace Backpack.Features
{
    public interface IFeature : IFeatureStateProvider
    {
        string Name { get; }
    }

    public interface IFeatureStateProvider
    {
        bool IsEnabled();
    }
}