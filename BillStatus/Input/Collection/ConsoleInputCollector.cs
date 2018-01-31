using System;
using System.Collections.Generic;
using CM332ABillStatus.Input.Validation;

namespace CM332ABillStatus.Input.Collection
{
    public class ConsoleInputCollector : IConsoleInputCollector
    {
        public IInputValidator Validator { get; set; }
        public string Prompt { get; set; } = string.Empty;
        public char InputMarker { get; set; } = '>';
        public uint RepeatPromptDelay { get; set; } = 3;
        internal bool NotCancelled { get; set; } = true;
        public IList<string> CancelCommands { get; set; } = new string[] {};

        public ConsoleInputCollector(string prompt, IInputValidator validator)
        {
            Validator = validator;
            Prompt = prompt;
        }
            
        public string GetInput()
        {
            var input = string.Empty;
            uint printPromptCounter = 0;
            while (!Validator.IsValid(input) && NotCancelled)
            {
                if (0 == printPromptCounter)
                {
                    Console.Clear();
                    PrintPrompt();
                    printPromptCounter = RepeatPromptDelay;
                }
                printPromptCounter--;
                Console.Write("{0} ", InputMarker);
                input = Console.ReadLine().Trim();
                if (CancelCommands.Contains(input))
                {
                    NotCancelled = false;
                    input = null;
                }
            }
            return input;
        }
        public void PrintPrompt()
        {
            Console.WriteLine(Prompt);
        }
    }
}

