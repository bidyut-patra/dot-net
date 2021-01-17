using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Riddles
{
    public class NumberRange
    {
        public int MaxValue { get; private set; }
        public int MinValue { get; private set; }
        public int Index { get; private set; }
        public int Difference
        {
            get
            {
                return this.MaxValue - this.MinValue;
            }
        }

        public bool IsGenerated
        {
            get
            {
                return this.Index == this.Difference;
            }
        }

        private Random Algorithm { get; set; }
        public int?[] Memory { get; set; }

        public NumberRange(int minValue, int maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;

            this.Algorithm = new Random();
            this.Memory = new int?[this.Difference];
            this.Index = 0;
        }

        public int? NextUnique()
        {
            int? number = this.Algorithm.Next(this.MinValue, this.MaxValue);
            while ((this.Index < this.Difference) && this.Memory.Contains((int)number))
            {
                number = this.Algorithm.Next(this.MinValue, this.MaxValue);
            }

            if (this.Index < this.Difference)
            {
                this.Memory[Index++] = (int)number;
            }
            else
            {
                number = null;
            }

            return number;
        }

        public bool ValueWithinRange(int value)
        {
            return value >= this.MinValue && value <= this.MaxValue;
        }

        public static NumberRange operator++(NumberRange range) 
        {
            return new NumberRange(range.MinValue + range.Difference, range.MaxValue + range.Difference);
        }
    }
}
