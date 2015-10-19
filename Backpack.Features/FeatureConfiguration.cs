using System.Configuration;
using System.Diagnostics;

namespace Backpack.Features
{
    public class FeatureConfiguration : ConfigurationSection
    {
        private const string SectionName = "backpack.features";

        #region Singleton Instance

        private static FeatureConfiguration _configuration;

        public static FeatureConfiguration Current
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = ConfigurationManager.GetSection(SectionName) as FeatureConfiguration;
                }

                return _configuration;
            }
        }

        #endregion

        private const string FeaturesElement = "features";

        [ConfigurationProperty(FeaturesElement, IsRequired = false)]
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public FeatureElementsCollection Features
        {
            get { return base[FeaturesElement] as FeatureElementsCollection; }
            internal set { base[FeaturesElement] = value; }
        }
    }
}