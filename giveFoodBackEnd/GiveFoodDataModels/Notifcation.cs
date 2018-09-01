using System;

namespace GiveFoodDataModels
{
    public class Notification
    {
        public long Id { get; set; }

        public Guid SendTo { get; set; }

        public bool IsRead { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
