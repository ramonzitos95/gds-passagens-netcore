using System.Collections.Generic;

namespace distribusion.api.client.Models.Basic
{
    public class BasicRelationshipObject : IDumpable
    {
        public BasicObject Data { get; set; }
    }

    public class ListBasicRelationshipObject : IDumpable
    {
        public List<BasicObject> Data { get; set; }
    }
}