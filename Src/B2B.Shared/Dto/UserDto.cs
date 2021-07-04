using System;
using B2B.Shared.Dto.Base;

namespace B2B.Shared.Dto
{
    public class UserDto : DtoBase
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string InGameName { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}
