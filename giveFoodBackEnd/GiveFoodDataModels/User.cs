using Microsoft.AspNetCore.Identity;

namespace GiveFoodDataModels
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public UserType Type { get; set; }

        public Document Document { get; set; }
    }

    public enum UserType
    {
        Admin,
        Giver,
        Taker,
    }
}



