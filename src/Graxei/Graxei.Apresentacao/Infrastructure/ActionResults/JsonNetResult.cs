using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Graxei.Apresentacao.Infrastructure.ActionResults
{
    public class JsonNetResult : ActionResult
    {
        public JsonNetResult()
        {
        }

        public JsonNetResult(object responseBody)
        {
            ResponseBody = responseBody;
        }

        public JsonNetResult(object responseBody, JsonSerializerSettings settings) : this(responseBody)
        {
            Settings = settings;
        }

        public JsonSerializerSettings Settings { get; set; }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object ResponseBody { get; set; }

        public static JsonNetResult GetWithDefaultFormatting(object responseBody)
        {
            return new JsonNetResult(responseBody, new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        private Formatting Formatting
        {
            get
            {
                return Debugger.IsAttached ? Formatting.Indented : Formatting.None;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!string.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (ResponseBody != null)
            {
                response.Write(JsonConvert.SerializeObject(ResponseBody, Formatting, Settings));
            }
        }
    }
}