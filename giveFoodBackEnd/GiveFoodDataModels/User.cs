using Microsoft.AspNetCore.Identity;
using System;

namespace GiveFoodDataModels
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }

        public UserType Type { get; set; }

        public string Description { get; set; }

        public UserStatus Status { get; set; }

    }
    public enum UserStatus
    {
        PendingApproval,
        Approved,
        NotApproved
    }
    public enum UserType
    {
        Admin,
        Giver,
        Taker,
    }
}



