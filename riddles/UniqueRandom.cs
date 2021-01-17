using System.Collections.Generic;

namespace Riddles
{
    public class UniqueRandom
    {
        public NumberRange Range { get; private set; }
        public List<NumberRange> SubRanges { get; private set; }

        private int NumberCount;

        public UniqueRandom(int minValue, int maxValue, int memorySize = 20000)
        {
            this.Range = new NumberRange(minValue, maxValue + 1);
            this.NumberCount = memorySize;
            this.SubRanges = new List<NumberRange>();

            this.ComputeSubRanges();
        }

        private void ComputeSubRanges()
        {
            if(this.NumberCount < this.Range.Difference)
            {
                var currentMinValue = this.Range.MinValue;
                var index = currentMinValue;
                for (; index <= this.Range.MaxValue; index++)
                {
                    if(index == (currentMinValue + this.NumberCount))
                    {
                        this.SubRanges.Add(new NumberRange(currentMinValue, currentMinValue + this.NumberCount));
                        currentMinValue = index + 1;
                    }
                }

                if(index > currentMinValue)
                {
                    this.SubRanges.Add(new NumberRange(currentMinValue, index));
                }
            }
            else
            {
                this.SubRanges.Add(new NumberRange(this.Range.MinValue, this.Range.MaxValue));
            }
        }

        public int? Next()
        {
            var currentRange = this.GetNextRange();
            if(currentRange != null)
            {
                var number = currentRange.NextUnique();
                return number;
            }
            return null;
        }

        private NumberRange GetNextRange()
        {
            NumberRange nextRange = null;
            foreach(var subRange in SubRanges)
            {
                if(subRange.IsGenerated == false)
                {
                    nextRange = subRange;
                    break;
                }
            }
            return nextRange;
        }
    }
}
