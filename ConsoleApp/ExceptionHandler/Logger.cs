using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class Logger
    {
        public static void Log(BaseException be)
        {
            var logWriter = new StreamWriter(Environment.CurrentDirectory + @"\log.txt");
            var message = be.GetMessage();
            logWriter.WriteLine(message);
            logWriter.Close();
            Console.WriteLine(message);
        } 
    }
}
