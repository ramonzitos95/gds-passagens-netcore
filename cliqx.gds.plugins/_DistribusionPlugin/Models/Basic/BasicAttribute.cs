namespace distribusion.api.client.Models.Basic
{
    public abstract class BasicAttribute : IDumpable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}