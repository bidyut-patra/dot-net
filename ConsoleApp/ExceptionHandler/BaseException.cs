using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class BaseException : ApplicationException
    {
        public string User { get; set; }
        public DateTime TimeStamp { get; set; }

        public BaseException()
        {
            this.User = "DUMMY";
            this.TimeStamp = DateTime.Now;
        }

        public virtual string GetMessage()
        {
            return this.TimeStamp.ToString() + " - [" + this.User + "]: ";
        }
    }
}
