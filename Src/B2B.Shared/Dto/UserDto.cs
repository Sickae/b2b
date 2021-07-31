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
        public string ApplicationJson { get; set; }
        public int? ApplicationFlowId { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? LockoutEnd { get; set; }
    }
}
