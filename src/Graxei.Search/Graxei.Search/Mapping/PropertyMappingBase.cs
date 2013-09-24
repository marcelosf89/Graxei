using System;
using System.Collections.Generic;

using NHibernate.Properties;

namespace Graxei.Search.Mapping {
    public class PropertyMappingBase {
        protected PropertyMappingBase(IGetter getter)
        {
            this.Getter = getter;
        }

        public IGetter Getter { get; private set; }
    }
}
