using CM332ABillStatus.Input.Validation;
using System;

namespace CM332ABillStatus.Input.Collection
{
    public interface IConsoleInputCollector
    {
        void PrintPrompt();
        string GetInput();
    }
}

