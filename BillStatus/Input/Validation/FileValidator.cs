using System;
using System.IO;
using System.Security;

namespace CM332ABillStatus.Input.Validation
{
    public class FileValidator : InputValidator
    {
        override public bool IsValid(string filePath)
        {
            var rVal = true;
            string fullPath = null;
            try {
                fullPath = Path.GetFullPath(filePath);

                if (Directory.Exists(fullPath) == true)
                {
                    rVal = false;
                }
                else if (File.Exists(fullPath) == false)
                {
                    File.Create(fullPath);
                    File.Delete(fullPath);
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

