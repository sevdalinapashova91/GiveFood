using GiveFoodDataModels;

namespace GiveFoodServices.Admin.Models
{
    public class UserDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public UserType Type { get; set; }

        public UserStatus Status { get; set; }

        public DocumentDto Document { get; set; }

        public string Description { get; set; }
    }
}
