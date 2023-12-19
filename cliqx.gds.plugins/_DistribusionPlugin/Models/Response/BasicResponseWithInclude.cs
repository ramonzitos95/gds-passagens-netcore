using System.Collections.Generic;
using distribusion.api.client.Models.Basic;

namespace distribusion.api.client.Response
{
    public abstract class BasicResponseWithInclude<TData, TIncluded> : BasicResponse<TData>
        where TData : BasicObject where TIncluded : BasicObject
    {
        public IEnumerable<TIncluded> Included { get; set; }
    }
}