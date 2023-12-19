namespace cliqx.gds.utils
{
    public class SnogException : Exception
    {
        public string Code { get; set; }
        public SnogException(string message, string code) : base(message)
        {
            Code = code;
        }
    }
}
