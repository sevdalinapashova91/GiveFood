using GiveFoodDataModels;

namespace GiveFoodServices.Users.Models
{
    public class UserDto
    {
        public string Name { get; set; }

        public UserType Type { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

