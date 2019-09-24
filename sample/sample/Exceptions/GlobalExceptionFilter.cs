using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace sample.Exceptions
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception;
            HttpResponseMessage response = new HttpResponseMessage();
            var message = exception.Message;
#if DEBUG
            message += exception.StackTrace;
#endif
            var content = new StringContent(message);
            response.Content = content;
            response.StatusCode = HttpStatusCode.InternalServerError;
            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }
    }
}