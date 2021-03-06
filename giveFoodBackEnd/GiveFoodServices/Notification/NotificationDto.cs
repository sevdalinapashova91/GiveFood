﻿using GiveFoodDataModels;
using System;

namespace GiveFoodServices.Users.Models
{
    public class NotificationDto
    {
        public long Id { get; set; }
        
        public bool IsRead { get; set; }

        public MessageType MessageType { get; set; }

        public DateTime DateCreated { get; set; }

        public string SenderName { get; set; }
    }
}
