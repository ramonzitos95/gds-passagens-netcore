namespace cliqx.gds.contract.Models.Dto
{
    public class ResultOperation
    {
        public bool Sucesso { get; set; }
        public string Message { get; set; }

        public static ResultOperation CriarSucesso(string message)
        {
            return new ResultOperation { Sucesso = true, Message = message };
        }

        public static ResultOperation CriarFalha(string message)
        {
            return new ResultOperation { Sucesso = false, Message = message };
        }

    }

    public class ResultOperationHttp
    {
        public string Message { get; set; }
        public string Code { get; set; }
       
        public static ResultOperationHttp CriarResult(string message, string code)
        {
            return new ResultOperationHttp { Message = message , Code = code};
        }

    }
}
