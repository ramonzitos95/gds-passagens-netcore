using System.Runtime.Serialization;

namespace cliqx.gds.utils
{
    public static class ErrorIntegrationGDS
    {
        /// <summary>
        /// Plugin não encontrado
        /// </summary>
        public static string PLUGIN_NOT_FOUND = "PLUGIN_NOT_FOUND";

        /// <summary>
        /// Assento indisponível ou problema de mapa de poltronas
        /// </summary>
        public static string SEAT_UNAVAILABLE = "SEAT_UNAVAILABLE";

        /// <summary>
        /// Erro para quando a poltrona está ocupada
        /// </summary>
        public static string SEAT_BUSY = "SEAT_BUSY";

        /// <summary>
        /// Erro para quando o parâmetro não informado (exemplo poltrona não informada)
        /// </summary>
        public static string PARAMETER_NOT_INFORMED = "PARAMETER_NOT_INFORMED";

        /// <summary>
        /// Erro para quando o parâmetro é informado, porém vem nulo ou branco
        /// </summary>
        public static string PARAMETER_IS_NULL_OR_BLANK = "PARAMETER_IS_NULL_OR_WHITE";

        /// <summary>
        /// Método não implementado
        /// </summary>
        public static string METHOD_NOT_IMPLEMENTED = "METHOD_NOT_IMPLEMENTED";
    }
}
