using System;
using System.Collections.Generic;
using Graxei.Search.Attributes;

namespace Graxei.Search.Mapping.Definition
{
    using Type = System.Type;

    public interface IClassBridgeDefinition
    {
        string Name                           { get; }
        Attributes.Store Store                { get; }
        Index Index                           { get; }
        Type Analyzer                         { get; }
        float Boost                           { get; }
        Type Impl                             { get; }
        Dictionary<string, object> Parameters { get; }
    }
}
