using System;
using System.Collections.Generic;

namespace CM332ABillStatus.Input.Validation
{
    public class StringListValidator : InputValidator
    {
        public ICollection<string> ValidOptions { get; set; }
        public bool CaseSensitive { get; set; }

        public StringListValidator(bool caseSensitive, params string[] validOptions)
        {
            CaseSensitive = caseSensitive;
            ValidOptions = validOptions;
        }
        public StringListValidator (params string[] validOptions) : this(true,validOptions) { }

        override public bool IsValid(string data)
        {
            var rVal = false;
            var comparison = (this.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase );
            var enumer = ValidOptions.GetEnumerator();

            while (rVal == false && enumer.MoveNext())
            {
                if (enumer.Current.Equals(data, comparison))
                {
                    rVal = true;
                }
            }

            return rVal;
        }
    }
}

