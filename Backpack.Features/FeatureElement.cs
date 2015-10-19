using System.Configuration;
using System.Diagnostics;

namespace Backpack.Features
{
    [DebuggerDisplay("Name: {Name}, IsEnabled: {IsEnabled}")]
    public class FeatureElement : ConfigurationElement
    {
        private const string NameAttribute = "name";

        [ConfigurationProperty(NameAttribute, IsKey = true, IsRequired = true)]
        public string Name
        {
            get { return base[NameAttribute] as string; }
            internal set { base[NameAttribute]  = value; }
        }

        private const string IsEnabledAttribute = "enabled";

        [ConfigurationProperty(IsEnabledAttribute, IsKey = true, IsRequired = true)]
        public bool IsEnabled
        {
            get { return (bool)base[IsEnabledAttribute]; }
            internal set { base[IsEnabledAttribute] = value; }
        }
    }
}