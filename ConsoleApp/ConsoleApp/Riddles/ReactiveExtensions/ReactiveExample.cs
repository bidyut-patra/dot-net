using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class ReactiveExample
    {
        public void Test()
        {
            var o = Observable.Generate(0,
                _ => true,
                i => i + 1,
                i => new string('#', i),
                i => TimeSelector(i));

            using(o.Subscribe(Console.WriteLine))
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadLine();
            }
        }

        private static TimeSpan TimeSelector(int i)
        {
            return TimeSpan.FromSeconds(i);
        }
    }
}
