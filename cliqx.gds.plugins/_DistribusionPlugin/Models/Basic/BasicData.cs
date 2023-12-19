using System.Collections.Generic;

namespace distribusion.api.client.Models.Basic
{
    public abstract class BasicObject<TAttribute, TRelationship> : BasicObject, IDumpable where TAttribute : BasicAttribute
    {
        public TAttribute Attributes { get; set; }
        public TRelationship Relationships { get; set; }
    }
}