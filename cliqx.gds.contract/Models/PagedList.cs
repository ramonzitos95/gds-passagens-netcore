using System.Collections.Generic;

namespace cliqx.gds.contract.Models
{
    public class PagedList<T>
    {
        public IEnumerable<T> Elements { get; set; }
        public bool StillFetchable { get; set; }
        public Guid ExternalId { get; set; } = Guid.NewGuid();
        public string ExtraData { get; set; }

        public PagedList(bool stillFetchable = false)
        {
            StillFetchable = stillFetchable;
            Elements = new List<T>();
        }

        public PagedList(IEnumerable<T> collection, bool stillFetchable = false) : this(stillFetchable)
        {
            Elements = collection;

        }

        public PagedList(int capacity, bool stillFetchable = false) : this(stillFetchable)
        {
            Elements = new List<T>(capacity);
            StillFetchable = stillFetchable;
        }
    }
}