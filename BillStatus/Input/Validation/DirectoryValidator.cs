using System;
using System.IO;
using System.Security;

namespace CM332ABillStatus.Input.Validation
{
    public class DirectoryValidator : InputValidator
    {
        override public bool IsValid(string directoryPath)
        {
            var rVal = true;
            string fullPath = null;
            try {
                fullPath = Path.GetFullPath(directoryPath);

                if (File.Exists(fullPath) == true)
                {
                    rVal = false;
                }
                else if (Directory.Exists(fullPath) == false)
                {
                    Directory.CreateDirectory(fullPath);
                    Directory.Delete(fullPath);
                }
            }
            catch (Exception e)
            {
                rVal = false;
                Console.WriteLine("An error occurred: {0}",e.Message);
            }

            return rVal;
        }
    }
}

