using System;

namespace CM332ABillStatus
{
    public class Bill
    {
        public long ActionCount { get; set; }

        public long AmendmentCount { get; set; }

        public string BillNumber { get; set; }

        public string BillType { get; set; }

        public long CboCostEstimateCount { get; set; }

        public long CommitteeReportCount { get; set; }

        public long CommitteeCount { get; set; }

        public string Congress { get; set; }

        public long CosponsorCount { get; set; }

        public bool IsByRequest { get; set; } = false;

        public long LawsCount { get; set; }

        public long RecordedVotesCount { get; set; }

        public long NotesCount { get; set; }

        public string OriginChamber { get; set; }

        public long PrimarySubjectCount { get; set; }

        public long RelatedBillCount { get; set; }

        public long SponsorCount { get; set; }

        public long SubjectCount { get; set; }

        public long SummaryCount { get; set; }

        public long TitleCount { get; set; }
    }
}