using Moq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Graxei.Apresentacao.Teste
{
    public class ApresentacaoTesteComum
    {
        public static ControllerContext SetupContext(string ip, Controller controller)
        {
            Mock<HttpRequestBase> request = new Mock<HttpRequestBase>(MockBehavior.Strict);
            request.Setup(req => req.UserHostAddress).Returns(ip);
            Mock<HttpContextBase> context = new Mock<HttpContextBase>();
            context.SetupGet(c => c.Request).Returns(request.Object);
            ControllerContext controllerContext = new ControllerContext(context.Object, new RouteData(), controller);
            return controllerContext;
        }
    }
}
