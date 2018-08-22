using GiveFoodDataModels;

namespace GiveFoodServices.Admin.Models
{
    public class EvaluateUserDto
    {
        public long DocumentId { get; set; }

        public string Email { get; set; }

        public UserType Type { get; set; }

        public bool IsApproved { get; set; }
    }
}
