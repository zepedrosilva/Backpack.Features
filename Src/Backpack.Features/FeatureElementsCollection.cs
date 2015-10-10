using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Backpack.Features
{
    [ConfigurationCollection(typeof(FeatureElement))]
    public class FeatureElementsCollection : ConfigurationElementCollection, IEnumerable<FeatureElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FeatureElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return (element as FeatureElement).Name;
        }

        #region IEnumerable<FeatureElement> Members

        public new virtual IEnumerator<FeatureElement> GetEnumerator()
        {
            return Enumerable.Range(0, Count).Select(i => BaseGet(i) as FeatureElement).GetEnumerator();
        }

        #endregion
    }
}