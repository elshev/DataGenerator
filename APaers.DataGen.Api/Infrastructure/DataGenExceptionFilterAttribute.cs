using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using APaers.DataGen.Abstract.Exceptions;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Api.Infrastructure
{
    public class DataGenExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private ILog Log { get; }

        public DataGenExceptionFilterAttribute(ILog log)
        {
            Log = log;
        }

        public override void OnException(HttpActionExecutedContext context)
        {
            string message = context.Exception.Message;
            if (context.Exception is DataGenExceptionBase dataGenException)
            {
                string reasonPhrase = dataGenException.ReasonPhrase;
                Log.Error($"Reason: '{reasonPhrase}'; Message: '{message}'");
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent(message),
                    ReasonPhrase = reasonPhrase
                });
            }

            Log.Error(message);

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(message),
                ReasonPhrase = "Critical exception"
            });
        }
    }
}