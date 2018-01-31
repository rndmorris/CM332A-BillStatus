using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CM332ABillStatus
{
    public class XmlHandler
    {
        private DirectoryInfo _xmlDirectory { get; set; }

        public XmlHandler(DirectoryInfo xmlDirectory)
        {
            _xmlDirectory = xmlDirectory;
        }

        public IList<Bill> ParseXmlFiles()
        {
            
            var xmlFiles = _xmlDirectory.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
            var bills = new List<Bill>(xmlFiles.Length);
            Console.WriteLine("Start parsing {0} XML files. This may take a while...", xmlFiles.Length);
            foreach (var xmlFile in xmlFiles)
            {
                var parser = new DataParser(xmlFile);
                var parsedBills = parser.ParseDocument();
                foreach (var parsedBill in parsedBills)
                {
                    bills.Add(parsedBill);
                }
            }
            Console.WriteLine("Completed XML Parsing process.");
            return bills;
        }
    }

    internal class DataParser
    {
        private FileInfo _xmlFile { get; set; }

        public DataParser(FileInfo xmlFile)
        {
            _xmlFile = xmlFile;
        }

        public IList<Bill> ParseDocument()
        {
            var doc = new XmlDocument();
            doc.Load(_xmlFile.FullName);

            var billNodes = doc.SelectNodes("/billStatus//bill");
            var bills = new List<Bill>(billNodes.Count);

            foreach (XmlNode billNode in billNodes)
            {
                var bill = new Bill();
                bill.ActionCount = billNode.SelectNodes("./actions//item").Count;
                bill.AmendmentCount = billNode.SelectNodes("./amendments//amendment").Count;
                bill.BillNumber = billNode.SelectSingleNode("./billNumber").InnerText;
                bill.BillType = billNode.SelectSingleNode("./billType").InnerText;
                bill.CboCostEstimateCount = billNode.SelectNodes("./cboCostEstimates//item").Count;
                bill.CommitteeCount = billNode.SelectNodes("./committees//billCommittees").Count;
                bill.CommitteeReportCount = billNode.SelectNodes("./committeeReports//committeeReport").Count;
                bill.Congress = billNode.SelectSingleNode("./congress").InnerText;
                bill.CosponsorCount = billNode.SelectNodes("./cosponsors//item").Count;

                bool isByRequestParseResult;
                bool.TryParse(billNode.SelectSingleNode("./isByRequest")?.InnerText, out isByRequestParseResult);
                bill.IsByRequest = isByRequestParseResult;

                bill.LawsCount = billNode.SelectNodes("./laws//item").Count;
                bill.NotesCount = billNode.SelectNodes("./notes//item").Count;
                bill.OriginChamber = billNode.SelectSingleNode("./originChamber").InnerText;
                bill.PrimarySubjectCount = billNode.SelectNodes("./primarySubject//parentSubject").Count;
                bill.RecordedVotesCount = billNode.SelectNodes("./recordedVotes//recordedVote").Count;
                bill.RelatedBillCount = billNode.SelectNodes("./relatedBills//item").Count;
                bill.SponsorCount = billNode.SelectNodes("./sponsors//item").Count;
                bill.SubjectCount = billNode.SelectNodes("./subjects//billSubjects").Count;
                bill.TitleCount = billNode.SelectNodes("./title").Count + billNode.SelectNodes("./titles//item").Count;

                bills.Add(bill);
            }

            return bills;
        }
    }
}