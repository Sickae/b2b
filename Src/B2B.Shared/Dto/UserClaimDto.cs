using System.Security.Claims;
using B2B.Shared.Dto.Base;

namespace B2B.Shared.Dto
{
    public class UserClaimDto : DtoBase
    {
        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public int UserId { get; set; }

        public Claim ToClaim()
        {
            return new Claim(ClaimType, ClaimValue);
        }
    }
}
