using System;
using System.IO;
using System.IO.Compression;

namespace CM332ABillStatus
{
    public class ZipExtractor
    {
        private DirectoryInfo _downloadDirectory;
        private DirectoryInfo _extractDirectory;
        public bool OverwriteExisting = false;

        public ZipExtractor(DirectoryInfo downloadDirectory, DirectoryInfo extractDirectory)
        {
            _downloadDirectory = downloadDirectory;
            _extractDirectory = extractDirectory;
        }

        public void ExtractAll()
        {
            Console.WriteLine("Starting extraction process...");
            var zipFiles = _downloadDirectory.GetFiles("*.zip", SearchOption.TopDirectoryOnly);
            foreach (var file in zipFiles)
            {
                Console.Write("  Extracting from {0}... ", file.Name);
                var archive = ZipFile.OpenRead(file.FullName);
                ExtractArchive(archive);
                Console.WriteLine("Extracted.");
            }
        }

        private void ExtractArchive(ZipArchive archive)
        {
            foreach (var entry in archive.Entries)
            {
                var endFile = new FileInfo(_extractDirectory.FullName + Path.DirectorySeparatorChar + entry.FullName);
                if (OverwriteExisting)
                {
                    if (endFile.Exists)
                        endFile.Delete();
                    entry.ExtractToFile(endFile.FullName);
                }
                else
                {
                    if (!endFile.Exists)
                        entry.ExtractToFile(endFile.FullName);
                }
            }
        }
    }
}