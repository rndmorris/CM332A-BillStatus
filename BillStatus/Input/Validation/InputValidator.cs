using System;

namespace CM332ABillStatus.Input.Validation
{
    public abstract class InputValidator : IInputValidator
    {
        abstract public bool IsValid(string input);
    }
}

