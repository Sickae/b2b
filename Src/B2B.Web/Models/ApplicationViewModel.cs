using System.ComponentModel;
using B2B.Shared.Attributes;
using B2B.Shared.Dto;

namespace B2B.Web.Models
{
    public class ApplicationViewModel : ApplicationFlowDto
    {
        [DisplayName("Your in-game name"), Placeholder, Required]
        public string InGameName { get; set; }

        [Required]
        public string FormJson { get; set; }
    }
}
