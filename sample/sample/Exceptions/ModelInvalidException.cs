using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sample.Exceptions
{
    public class ModelInvalidException : ApplicationException
    {
        public ModelInvalidException(): base(Resource.ModelInvalidError)
        {

        }
    }
}