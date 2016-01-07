using Newtonsoft.Json;
using System;
using System.Linq;

namespace Graxei.Transversais.ContratosDeDados.Serializacao
{
    public class DataRFC3339Converter : JsonConverter
    {
        private readonly Type[] _types;


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException("Unnecessary because CanRead is false. The type will skip the converter.");
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }
    }
}
