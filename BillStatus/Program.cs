using CM332ABillStatus.Input.Collection;
using CM332ABillStatus.Input.Validation;
using System;
using System.Collections.Generic;
using System.IO;

namespace CM332ABillStatus
{
    class Program
    {
        private SettingsFile _settings { get; set; }

        public static void Main(string[] args)
        {
            new Program().Run();
        }
        public Program()
        {
            this._settings = SettingsFile.Settings;
        }
        public void Run()
        {
            var collector = new ConsoleInputCollector(string.Format(
                "Select an option:{0}"+
                "0: Exit                      4: Load data from xml files{0}"+
                "1: View and edit settings    5: Generate .arff file{0}"+
                "2: Download data .zip files  6: RUN 2 THROUGH 5{0}"+
                "3: Extract zip files{0}"
                ,Environment.NewLine),new LongRangeValidator(0,6));
            
            var input = -1;
            IList<Bill> bills = null;
            while (input.Equals(0) == false)
            {
                input = int.Parse(collector.GetInput());
             
                switch(input)
                {
                    case 1:
                        _settings.ManageSettings();
                        break;
                    case 2:
                        RunDownload();
                        break;
                    case 3:
                        RunExtract();
                        break;
                    case 4:
                        bills = LoadBillData();
                        break;
                    case 5:
                        WriteArffFile(bills);
                        break;
                    case 6:
                        RunDownload();
                        RunExtract();
                        bills = LoadBillData();
                        WriteArffFile(bills);
                        break;
                    default:
                        break;
                }
            }
        }
        private void RunDownload()
        {
            Console.WriteLine(
                "Download directory:{0}{1}{0}Overwrite existing: {2}",
                Environment.NewLine,
                _settings.DownloadDirectoryPath,
                _settings.OverwriteDownloadedZipFiles
            );
            try{
                var directory = new DirectoryInfo(_settings.DownloadDirectoryPath);

                if (directory.Exists == false)
                    directory.Create();
                
                var download = new Downloader(directory);
                download.OverwriteExisting = _settings.OverwriteDownloadedZipFiles;
                download.DownloadAllFiles();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    "An error occured during the download process:{0}{1}"
                    ,Environment.NewLine
                    ,ex.Message);
            }
        }
        private void RunExtract()
        {
            Console.WriteLine(
                "Extract directory:{0}{1}{0}Overwrite existing: {2}",
                Environment.NewLine,
                _settings.ExtractDirectoryPath,
                _settings.OverwriteExtractedXmlFiles
            );
            var downloads = new DirectoryInfo(_settings.DownloadDirectoryPath);
            var extracts = new DirectoryInfo(_settings.ExtractDirectoryPath);

            if (extracts.Exists == false)
                extracts.Create();
            
            var zipExtractor = new ZipExtractor(downloads, extracts);
            zipExtractor.OverwriteExisting = _settings.OverwriteExtractedXmlFiles;
            zipExtractor.ExtractAll();
        }
        private IList<Bill> LoadBillData()
        {
            Console.WriteLine(
                "Trying to load from files in directory:{0}{1}",
                Environment.NewLine,
                _settings.ExtractDirectoryPath
            );
            var directory = new DirectoryInfo(_settings.ExtractDirectoryPath);
            var xmlHandler = new XmlHandler(directory);
            return xmlHandler.ParseXmlFiles();
        }
        private void WriteArffFile(IList<Bill> bills)
        {
            Console.WriteLine(
                "Trying to write bill data to:{0}{1}",
                Environment.NewLine,
                _settings.OutputArffFilePath
            );
            if (bills != null)
            {
                var file = new FileInfo(_settings.OutputArffFilePath);

                var arffWriter = new ArffWriter(bills, file);
                arffWriter.WriteFile();
            }
            else
            {
                Console.WriteLine("Error: Billstatus data must be loaded before a .arff file can be generated.");
            }
        }
    }
}