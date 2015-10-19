using System.Diagnostics;

namespace Backpack.Features.Tests
{
    [DebuggerDisplay("Feature: {Name}, Enabled: {IsEnabled()}")]
    public class TestFeature : IFeature
    {
        public TestFeature(bool initialState = true)
        {
            Name = GetType().ToString();
            State = initialState;
        }

        public string Name { get; set; }

        public bool State { get; set; }

        public bool IsEnabled()
        {
            return State;
        }
    }

    public class TestFeature2 : TestFeature
    {
        // Nothing to do here
    }

    public class TestFeature3 : TestFeature2
    {
        // Nothing to do here
    }
}