using System;
using System.Web.Http.ExceptionHandling;
using APaers.DataGen.Abstract.Repo;

namespace APaers.DataGen.Api.Infrastructure
{
    /// <summary>
    /// Global error handler
    /// </summary>
    public class DataGenExceptionHandler : ExceptionHandler
    {
        private ILog Log { get; }

        public DataGenExceptionHandler(ILog log)
        {
            Log = log;
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            try
            {
                Log.Error(context.Exception.Message);
            }
            catch (Exception)
            {
                // Suppress exceptions if they happened during logging
            }
            base.Handle(context);
        }
    }
}