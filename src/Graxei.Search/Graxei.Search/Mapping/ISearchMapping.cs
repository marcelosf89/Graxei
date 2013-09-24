using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NHibernate.Cfg;
using Graxei.Search.Mapping.Definition;

namespace Graxei.Search.Mapping
{
    using Type = System.Type;

    public interface ISearchMapping
    {
        ICollection<DocumentMapping> Build(Configuration cfg);
    }
}
