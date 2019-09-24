using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sample.Exceptions
{
    public class CompareDateException : ApplicationException
    {
        public CompareDateException(): base(Resource.StartEndDateError)
        {

        }
    }
}