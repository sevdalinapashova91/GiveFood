namespace GiveFoodServices.Users.Models
{
    public class ProfileDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string DocumentName { get; set; }

        public bool IsApproved { get; set; }
    }
}
