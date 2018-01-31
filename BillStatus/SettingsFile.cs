using CM332ABillStatus.Input.Collection;
using CM332ABillStatus.Input.Validation;
using System;
using System.Configuration;
using System.IO;

namespace CM332ABillStatus
{
    public class SettingsFile : ApplicationSettingsBase
    {
        private static SettingsFile _settings;
        public static SettingsFile Settings
        {
            get
            { 
                if (_settings == null)
                    _settings = new SettingsFile();
                return _settings;
            }
        }

        public void ManageSettings()
        {
            var menuInput = new ConsoleInputCollector(string.Format(
                "Select an option:{0}"
                +"0: Back to main menu         3: Output file location{0}"
                +"1: Download directory        4: Overwrite downloaded files{0}"
                +"2: Data extract directory    5: Overwrite extracted files{0}"
                ,Environment.NewLine),new LongRangeValidator(0,5));

            var input = -1;
            while (input.Equals(0) == false)
            {
                var newSetting = string.Empty;
                ConsoleInputCollector settingCollector = null;

                input = int.Parse(menuInput.GetInput());

                switch (input)
                {
                    case 1:
                        settingCollector = new ConsoleInputCollector(
                             string.Format(
                                 "Where would you like to store downloaded data? (Leave blank to cancel){0}" +
                                 "Current: {1}",
                                 Environment.NewLine, this.DownloadDirectoryPath),
                             new DirectoryValidator()
                         );
                        settingCollector.CancelCommands = new[] { string.Empty };
                        newSetting = settingCollector.GetInput();
                        if (newSetting != null )
                            this.DownloadDirectoryPath = newSetting;
                        break;


                    case 2:
                        settingCollector = new ConsoleInputCollector(
                            string.Format(
                                "Where would you like to store extracted data? (Leave blank to cancel){0}"+
                                "Current: {1}",
                                Environment.NewLine,this.ExtractDirectoryPath),
                            new DirectoryValidator()
                        );
                        settingCollector.CancelCommands = new[] { string.Empty };
                        newSetting = settingCollector.GetInput();
                        if (newSetting != null )
                            this.ExtractDirectoryPath = newSetting;
                        break;


                    case 3:
                        settingCollector = new ConsoleInputCollector(
                            string.Format(
                                "Where would you like to save the output.arff file? (Leave blank to cancel){0}"+
                                "Current: {1}",
                                Environment.NewLine,this.OutputArffFilePath),
                            new FileValidator()
                        );
                        settingCollector.CancelCommands = new[] { string.Empty };
                        newSetting = settingCollector.GetInput();
                        if (newSetting != null )
                            this.OutputArffFilePath = newSetting;
                        break;


                    case 4:
                        settingCollector = new ConsoleInputCollector(
                            string.Format(
                                "Overwrite pre-existing zip files? (Leave blank to cancel){0}"+
                                "Current: {1}",
                                Environment.NewLine,this.OverwriteDownloadedZipFiles),
                            new BoolValidator()
                        );
                        settingCollector.CancelCommands = new[] { string.Empty };
                        newSetting = settingCollector.GetInput();
                        if (newSetting != null )
                            this.OverwriteDownloadedZipFiles = bool.Parse(newSetting);
                        break;


                    case 5:
                        settingCollector = new ConsoleInputCollector(
                            string.Format(
                                "Overwrite pre-existing xml data files? (Leave blank to cancel){0}"+
                                "Current: {1}",
                                Environment.NewLine,this.OverwriteExtractedXmlFiles),
                            new BoolValidator()
                        );
                        settingCollector.CancelCommands = new[] { string.Empty };
                        newSetting = settingCollector.GetInput();
                        if (newSetting != null )
                            this.OverwriteExtractedXmlFiles = bool.Parse(newSetting);
                        break;


                    default:
                        break;
                }
            }
            _settings.Save();
        }

        private SettingsFile() {}
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("billstatus_downloads")]
        public string DownloadDirectoryPath
        {
            get { return (string)this["DownloadDirectoryPath"]; }
            set { this["DownloadDirectoryPath"] = value; }
        }
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("billstatus_extracts")]
        public string ExtractDirectoryPath
        {
            get { return (string)this["ExtractDirectoryPath"]; }
            set { this["ExtractDirectoryPath"] = value; }
        }
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("BillStatus.arff")]
        public string OutputArffFilePath
        {
            get { return (string)this["OutputArffFilePath"]; }
            set { this["OutputArffFilePath"] = value; }
        }
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("False")]
        public bool OverwriteDownloadedZipFiles
        {
            get { return (bool)this["OverwriteDownloadedZipFiles"]; }
            set { this["OverwriteDownloadedZipFiles"] = value; }
        }
        [UserScopedSettingAttribute()]
        [DefaultSettingValueAttribute("True")]
        public bool OverwriteExtractedXmlFiles
        {
            get { return (bool)this["OverwriteExtractedXmlFiles"]; }
            set { this["OverwriteExtractedXmlFiles"] = value; }
        }
    }
}

