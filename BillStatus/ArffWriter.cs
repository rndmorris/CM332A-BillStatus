using System;
using System.Collections.Generic;
using System.IO;

namespace CM332ABillStatus
{
    public class ArffWriter
    {
        private IList<Bill> _bills;
        private FileInfo _arffOutputFile;

        public ArffWriter(IList<Bill> bills, FileInfo arffOutputFile)
        {
            _bills = bills;
            _arffOutputFile = arffOutputFile;
        }

        public void WriteFile()
        {
            Console.WriteLine("Preparing to write {0} entries to {1}...", _bills.Count, _arffOutputFile.FullName);
            if (_arffOutputFile.Exists)
                _arffOutputFile.Delete();
            if (!_arffOutputFile.Directory.Exists)
                _arffOutputFile.Directory.Create();
			
            using (var writer = new StreamWriter(_arffOutputFile.FullName, true))
            {
                writer.AutoFlush = true;

                WriteFileHeader(writer);
                foreach (var bill in _bills)
                {
                    //for (var i = 0; i < 10; i++)
                    {
                        //var bill = _bills [i];
                        var formatString = "{0,4}, {1,4}, {2,4}, {3,4}, {4,4}, {5,4}, {6,4}, {7,4}, {8,4}, {9,4}, {10,4}, {11,4}, {12,4}, {13,4}, {14,4}, {15,4}, {16,4}, {17,4}, {18,4}";
                        writer.WriteLine(
                            formatString,
                            bill.ActionCount,
                            bill.AmendmentCount,
                            bill.BillNumber,
                            bill.BillType.ToLower(),
                            bill.CboCostEstimateCount,
                            bill.CommitteeCount,
                            bill.CommitteeReportCount,
                            bill.Congress.ToLower(),
                            bill.CosponsorCount,
                            bill.IsByRequest.ToString().ToLower(),
                            bill.LawsCount,
                            bill.NotesCount,
                            bill.OriginChamber.ToLower(),
                            bill.PrimarySubjectCount,
                            bill.RecordedVotesCount,
                            bill.RelatedBillCount,
                            bill.SponsorCount,
                            bill.SubjectCount,
                            bill.TitleCount
                        );
                    }
                }
                Console.WriteLine("Completed writing file.");
            }
        }

        private void WriteFileHeader(StreamWriter w)
        {
            w.WriteLine(@"@RELATION BillStatus");
            w.WriteLine();
            w.WriteLine(@"@ATTRIBUTE actionCount          numeric");
            w.WriteLine(@"@ATTRIBUTE amendmentCount       numeric");
            w.WriteLine(@"@ATTRIBUTE billNumber           numeric");
            w.WriteLine(@"@ATTRIBUTE billType             {hconres, hjres, hres, hr, sconres, sjres, sres, s}");
            w.WriteLine(@"@ATTRIBUTE cboCostEstimateCount numeric");
            w.WriteLine(@"@ATTRIBUTE committeeCount       numeric");
            w.WriteLine(@"@ATTRIBUTE committeeReportCount numeric");
            w.WriteLine(@"@ATTRIBUTE congress             {113, 114, 115}");
            w.WriteLine(@"@ATTRIBUTE cosponsorCount       numeric");
            w.WriteLine(@"@ATTRIBUTE isByRequest          {true, false}");
            w.WriteLine(@"@ATTRIBUTE lawsCount            numeric");
            w.WriteLine(@"@ATTRIBUTE notesCount           numeric");
            w.WriteLine(@"@ATTRIBUTE originChamber        {senate, house}");
            w.WriteLine(@"@ATTRIBUTE primarySubjectCount  numeric");
            w.WriteLine(@"@ATTRIBUTE recordedVotesCount   numeric");
            w.WriteLine(@"@ATTRIBUTE relatedBillCount     numeric");
            w.WriteLine(@"@ATTRIBUTE sponsorCount         numeric");
            w.WriteLine(@"@ATTRIBUTE subjectCount         numeric");
            w.WriteLine(@"@ATTRIBUTE titleCount           numeric");
            w.WriteLine();
            w.WriteLine();
            w.WriteLine(@"@DATA");
        }
    }
}