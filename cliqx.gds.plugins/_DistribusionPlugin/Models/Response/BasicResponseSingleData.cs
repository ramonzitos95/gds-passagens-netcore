using System;
using System.Collections.Generic;
using distribusion.api.client.Models;
using distribusion.api.client.Models.Basic;

namespace distribusion.api.client.Response
{
    public abstract class BasicResponseSingleData<TData> where TData : BasicObject, IDumpable
    {
        public TData Data { get; set; }
        public JsonApi JsonApi { get; set; }
        public Meta Meta { get; set; }
        public IEnumerable<dynamic> Included { get; set; }
    }
}
