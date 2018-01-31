using System;

namespace CM332ABillStatus.Input.Validation
{
    public class BoolValidator : InputValidator
    {
        override public bool IsValid(string data)
        {
            var rVal = true;
            bool parsed;
            if (!(bool.TryParse(data?.ToLowerInvariant(), out parsed)))
            {
                rVal = false;
            }
            return rVal;
        }
    }
}

