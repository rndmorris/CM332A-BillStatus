using System;
using System.Collections.Generic;
using System.IO;
using Dictionary = System.Collections.Generic.Dictionary<string,string>;

namespace BillStatus
{
    class Driver
    {
        private Dictionary<string,string> Settings { get; set; } = new Dictionary<string,string>();

        public static void Main(string[] args)
        {
            new Driver().Run();
        }

        public Driver()
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Settings.Add("DownloadDirectoryPath", (desktop + Path.DirectorySeparatorChar + "billstatus_downloads"));
            Settings.Add("ExtractDirectoryPath", (desktop + Path.DirectorySeparatorChar + "billstatus_extracts"));
            Settings.Add("OutputArffFilePath", (desktop + Path.DirectorySeparatorChar + "billstatus.arff"));
            Settings.Add("OverwriteDownloadedZipFiles", "false");
            Settings.Add("OverwriteExtractedXmlFiles", "false");
        }

        public void Run()
        {
            Console.WriteLine("Executing with the current settings:");
            foreach (KeyValuePair<string,string> pair in Settings)
            {
                Console.WriteLine("  {0}:", pair.Key);
                Console.WriteLine("    {0}", pair.Value);
            }
            RunDownload(FileOverwriteMode.PreserveAll);
            RunExtract(FileOverwriteMode.PreserveAll);
            var bills = LoadBillData();
            WriteArffFile(bills);
            Console.WriteLine("Execution complete. Press any key to continue...");
            Console.ReadKey();
        }

        private void RunDownload(FileOverwriteMode overwriteMode)
        {
            var directory = new DirectoryInfo(Settings["DownloadDirectoryPath"]);
            if (!directory.Exists)
                directory.Create();
            var download = new Downloader(directory);
            download.FileOverwriteMode = overwriteMode;
            download.DownloadAllFiles();
        }

        private void RunExtract(FileOverwriteMode overwriteMode)
        {
            var downloads = new DirectoryInfo(Settings["DownloadDirectoryPath"]);
            var extracts = new DirectoryInfo(Settings["ExtractDirectoryPath"]);
            if (!extracts.Exists)
                extracts.Create();
            var zipExtractor = new ZipExtractor(downloads, extracts);
            zipExtractor.FileOverwriteMode = overwriteMode;
            zipExtractor.ExtractAll();
        }

        private IList<Bill> LoadBillData()
        {
            var directory = new DirectoryInfo(Settings["ExtractDirectoryPath"]);
            var xmlHandler = new XmlHandler(directory);
            return xmlHandler.ParseXmlFiles();
        }

        private void WriteArffFile(IList<Bill> bills)
        {
            if (bills != null)
            {
                var file = new FileInfo(Settings["OutputArffFilePath"]);

                var arffWriter = new ArffWriter(bills, file);
                arffWriter.WriteFile();
            }
            else
            {
                Console.WriteLine("Billstatus data must be loaded before a .arff file can be generated.");
            }
        }
    }
}