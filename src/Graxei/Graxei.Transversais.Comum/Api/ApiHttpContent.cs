using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Graxei.Transversais.Comum.Api
{
    public class ApiHttpContent
    {
        public static string CriarJson<T>(T t) where T : class
        {
             return JsonConvert.SerializeObject(t, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.FFF'Z'" });
        }

        public static HttpContent Criar<T>(T t) where T : class
        {
            string json = CriarJson(t);
            return Criar(json);
        }

        public static HttpContent Criar(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static T Desserializar<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject<T>(json, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss.FFF'Z'" });
        }

        public static bool ResponseOk(HttpResponseMessage message)
        {
            if (message == null || message.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            StringContent json = (StringContent)message.Content;
            ResponseContrato responseContrato = Desserializar<ResponseContrato>(json.ToString());
            return (responseContrato != null && responseContrato.Ok);
        }
    }
}
