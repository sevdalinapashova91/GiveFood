using System;

namespace GiveFoodDataModels
{
    public class Notification
    {
        public long Id { get; set; }

        public Guid SendTo { get; set; }

        public bool IsRead { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid Creator { get; set; }

        public string CreatorName { get; set; }

        public MessageType MessageType { get; set; }
    }

    public enum MessageType
    {
        Invalid,
        AdminApprovalPending,
        TakerApprovalPending,
        GiverApprovalPending,
        Rejected,
        Approved,
    }
}
