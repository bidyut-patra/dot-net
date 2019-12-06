using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class FileAccessException : BaseException
    {
        public string FileName { get; set; }

        public override string GetMessage()
        {
            var baseMessage = base.GetMessage();
            baseMessage += "access denied to " + this.FileName; // baseMessage = baseMessage + ....
            return baseMessage;
        }
    }
}
