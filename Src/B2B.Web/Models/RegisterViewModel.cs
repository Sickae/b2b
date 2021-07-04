using B2B.Shared.Dto;

namespace B2B.Web.Models
{
    public class RegisterViewModel : UserDto
    {
        public string Password { get; set; }
        public string PasswordRe { get; set; }
    }
}
