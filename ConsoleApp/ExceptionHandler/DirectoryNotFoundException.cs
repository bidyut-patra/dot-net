using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class DirectoryNotFoundException : BaseException
    {
        public string Directory { get; set; }

        public override string GetMessage()
        {
            var baseMessage = base.GetMessage();
            baseMessage += "access denied to directory" + this.Directory; // baseMessage = baseMessage + ....
            return baseMessage;
        }
    }
}
