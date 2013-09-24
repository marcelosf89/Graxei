using System.Collections.Generic;
using Graxei.Search.Bridge;
using Graxei.Search.Bridge.Builtin;

namespace Graxei.Search.Tests.Bridge
{
    public class TruncateStringBridge : StringBridge, IParameterizedBridge
    {
        private int div;

        #region IParameterizedBridge Members

        public void SetParameterValues(Dictionary<string, object> parameters)
        {            
            div = parameters["dividedBy"] == null ? 0 : (int) parameters["dividedBy"];
        }

        #endregion

        public override object StringToObject(string stringValue)
        {
            return stringValue;
        }

        public override string ObjectToString(object obj)
        {
            string str = obj as string;
            return str != null ? str.Substring(0, str.Length / div) : null;
        }
    }
}
