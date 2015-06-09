using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
