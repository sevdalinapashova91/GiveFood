using GiveFoodDataModels;

namespace GiveFoodServices.Admin.Models
{
    public class EvaluateUserDto
    {
        public string Email { get; set; }

        public UserType Type { get; set; }

        public bool IsApproved { get; set; }
    }
}
