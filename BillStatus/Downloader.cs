using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BillStatus
{
    class Downloader
    {
        private System.Collections.Generic.IList<string> _urls { get; set; }

        private DirectoryInfo _downloadDirectory { get; set; }

        public FileOverwriteMode FileOverwriteMode { get; set; } = FileOverwriteMode.PreserveAll;

        public Downloader(DirectoryInfo downloadDirectory)
        {
            _downloadDirectory = downloadDirectory;
            _urls = GetUrls();
        }

        private System.Collections.Generic.IList<string> GetUrls()
        {
            var baseUrl = "https://www.gpo.gov/fdsys/bulkdata/BILLSTATUS/{0}/{1}/BILLSTATUS-{0}-{1}.zip";
            var congresses = new []{ "113", "114", "115" };
            var types = new []{ "sres", "sjres", "sconres", "s", "hres", "hr", "hres", "hjres", "hconres" };
            var output = new List<string>();
            foreach (var congress in congresses)
            {
                foreach (var type in types)
                {
                    output.Add(string.Format(baseUrl, congress, type));
                }
            }
            return output;
        }

        public void DownloadAllFiles()
        {
            Console.WriteLine("Starting download process...");
            using (var client = new WebClient())
            {
                foreach (var url in _urls)
                {
                    var endFile = new FileInfo(_downloadDirectory.FullName + Path.DirectorySeparatorChar + GetFileNameFromURL(url));
                    DownloadFile(client, url, endFile);
                }
            }
            Console.WriteLine("Completed download process.");
        }

        private void DownloadFile(WebClient client, string url, FileInfo endFile)
        {
            Console.Write("  Downloading {0}... ", endFile.Name);
            if (FileOverwriteMode == FileOverwriteMode.OverwriteAll)
            {
                if (endFile.Exists)
                    endFile.Delete();
                client.DownloadFile(url, endFile.FullName);
                Console.WriteLine("Downloaded.");
            }
            else if (FileOverwriteMode == FileOverwriteMode.PreserveAll)
            {
                if (!endFile.Exists)
                {
                    client.DownloadFile(url, endFile.FullName);
                    Console.WriteLine("Downloaded.");
                }
                else
                    Console.WriteLine("Already exists, skipping.");
            }
        }

        private string GetFileNameFromURL(string url)
        {
            return url.Substring(url.LastIndexOf('/') + 1);
        }
    }
}