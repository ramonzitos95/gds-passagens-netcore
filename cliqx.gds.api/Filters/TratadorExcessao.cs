using cliqx.gds.contract.Models.Dto;
using System.Net;
using System.Web.Http.Filters;

namespace cliqx.gds.api.Filters
{
    public static class TratadorExcessao
    {
        public static ResultOperation TrataExcessao(Exception exception)
        {
            HttpStatusCode statusCode;
            string message;

            if (exception is NullReferenceException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = exception.Message;
            }
            else if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
                message = exception.Message;
            }
            else if (exception is ArgumentException)
            {
                statusCode = HttpStatusCode.UnprocessableEntity;
                message = exception.Message;
            }
            else if (exception is NotImplementedException)
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
            return ResultOperation.CriarFalha(message);
        }
    }
}
