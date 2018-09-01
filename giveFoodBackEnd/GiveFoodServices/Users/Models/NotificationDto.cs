using System;

namespace GiveFoodServices.Users.Models
{
    public class NotificationDto
    {
        public long Id { get; set; }
        
        public bool IsRead { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
