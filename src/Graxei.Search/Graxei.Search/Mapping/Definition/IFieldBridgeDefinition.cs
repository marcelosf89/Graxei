using System;
using System.Collections.Generic;

namespace Graxei.Search.Mapping.Definition {
    public interface IFieldBridgeDefinition 
    {
        System.Type Impl                      { get; }
        Dictionary<string, object> Parameters { get; }
    }
}
