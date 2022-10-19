using System;
using ContainerizedServiceClient.ServiceReference1;

namespace ContainerizedServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new SampleServiceClient())
            {
                ConsoleKeyInfo key = new ConsoleKeyInfo('E', ConsoleKey.Enter, false, false, false);
                while ((key.Modifiers != ConsoleModifiers.Control) && (key.Key != ConsoleKey.Z))
                {
                    Console.Write("Enter an integer = ");
                    var text = Console.ReadLine();

                    int number;
                    if(int.TryParse(text, out number))
                    {
                        var result = client.GetData(number);
                        Console.WriteLine("Response from service = {0}", result);
                    }
                    else
                    {
                        Console.WriteLine("Input is in incorrect format");
                    }

                    Console.WriteLine("Enter Ctrl+Z to exit...");
                    key = Console.ReadKey();
                }
            }              
        }
    }
}
