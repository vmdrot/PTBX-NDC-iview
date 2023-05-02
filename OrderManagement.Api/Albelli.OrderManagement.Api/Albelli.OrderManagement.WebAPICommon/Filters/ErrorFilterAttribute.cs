using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Albelli.OrderManagement.WebAPICommon
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ArgumentException)
                FormatResult(context, HttpStatusCode.BadRequest);
            else if (context.Exception is NullReferenceException)
                FormatResult(context, HttpStatusCode.NotFound);
            else
                FormatResult(context, HttpStatusCode.InternalServerError);
        }

        private static void FormatResult(HttpActionExecutedContext context, HttpStatusCode statusCode)
        {
            context.Response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(context.Exception.ToString())
            };
        }
    }
}
