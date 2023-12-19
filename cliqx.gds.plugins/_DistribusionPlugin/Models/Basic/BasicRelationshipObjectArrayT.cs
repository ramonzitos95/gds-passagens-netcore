using System.Collections.Generic;

namespace distribusion.api.client.Models.Basic
{

    public class BasicRelationshipObjectArrayT<TArrayDataType>
    {
        public IEnumerable<TArrayDataType> Data { get; set; }
    }

}