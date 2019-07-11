using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Core
{
    public interface IBaseEntity
    {
        string CreatedBy { get; }
        DateTime CreatedDate { get; }
        string ModifiedBy { get; }
        DateTime ModifiedDate { get; }
    }
}
