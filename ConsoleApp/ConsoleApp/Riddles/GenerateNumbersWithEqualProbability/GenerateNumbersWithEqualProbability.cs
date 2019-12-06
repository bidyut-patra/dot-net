using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Riddles
{
    public class GenerateNumbersWithEqualProbability : IAlgorithm<int[], double[]>
    {
        public double[] Compute(int[] input)
        {
            int x = 0, y = 0, z = 0;
            for (var i = 0; i < 100000; i++)
            {
                var n = this.generate();
                if (n == 0) x++;
                if (n == 1) y++;
                if (n == 2) z++;
            }

            double[] percentages = new double[input.Length];

            percentages[0] = x / 1000.0;
            percentages[1] = y / 1000.0;
            percentages[2] = z / 1000.0;

            return percentages;
        }

        private int i = 0;
        private int generate()
        {
            //var x = this.random();
            //var y = this.random();

            //return ((x == 0) && (y == 1)) ? generate() : x + y;

            int[] n = { 0, 1, 2 };

            if(i == 2)
            {

            }

            return n[i++];
        }

        private int random()
        {
            var rand = new Random();
            return rand.Next() % 2;
        }
    }
}
