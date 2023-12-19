using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Web.Http.Filters;

public class HttpStatusCodeExceptionFilterAttribute : System.Web.Http.Filters.ExceptionFilterAttribute
{
    public override void OnException(HttpActionExecutedContext context)
    {
        HttpStatusCode statusCode;
        string message;

        if (context.Exception is NullReferenceException)
        {
            statusCode = HttpStatusCode.BadRequest;
            message = context.Exception.Message;
        }
        else if(context.Exception is UnauthorizedAccessException)
        {
            statusCode = HttpStatusCode.Unauthorized; 
            message = context.Exception.Message;
        }
        else if (context.Exception is ArgumentException)
        {
            statusCode = HttpStatusCode.UnprocessableEntity; 
            message = context.Exception.Message;
        }
        else if (context.Exception is NotImplementedException)
        {
            statusCode = HttpStatusCode.NotImplemented;
            message = "Este método ainda não foi implementado.";
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError; // Define um código de status padrão para outras exceções.
            message = "Ocorreu um erro interno no servidor.";
        }

        // Cria uma resposta HTTP com o código de status e a mensagem adequados.
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(message),
            ReasonPhrase = message
        };

        context.Response = response;
    }
}
