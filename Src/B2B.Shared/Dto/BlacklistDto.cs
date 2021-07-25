using B2B.Shared.Dto.Base;

namespace B2B.Shared.Dto
{
    public class BlacklistDto : DtoBase
    {
        public string InGameName { get; set; }

        public string Reason { get; set; }

        public int AddedById { get; set; }
    }
}
