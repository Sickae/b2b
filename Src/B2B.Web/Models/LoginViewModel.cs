using B2B.Shared.Attributes;

namespace B2B.Web.Models
{
    public class LoginViewModel
    {
        [Required, ShortStringLength]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
