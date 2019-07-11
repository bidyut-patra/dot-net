using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Core
{
    public class BaseEntity<T> : IBaseEntity
    {
        public T Id { get; internal set; }
        public string CreatedBy { get; internal set; }
        public DateTime CreatedDate { get; internal set; }
        public string ModifiedBy { get; internal set; }
        public DateTime ModifiedDate { get; internal set; }
    }
}
