using System;

namespace CM332ABillStatus.Input.Validation
{
    public interface IInputValidator
    {
        bool IsValid(string input);
    }
}

