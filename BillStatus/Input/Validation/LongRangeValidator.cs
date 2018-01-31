using System;

namespace CM332ABillStatus.Input.Validation
{
    public class LongRangeValidator : InputValidator
    {
        public long MaxValue { get; set; }
        public long MinValue { get; set; }

        public LongRangeValidator(long minValue = long.MinValue, long maxValue = long.MaxValue)
        {
            MaxValue = maxValue;
            MinValue = minValue;
        }
        override public bool IsValid(string data)
        {
            var rVal = true;
            long parsed;
            if (!(long.TryParse(data, out parsed) && (MinValue <= parsed && parsed <= MaxValue)))
            {
                rVal = false;
            }
            return rVal;
        }
    }
}

