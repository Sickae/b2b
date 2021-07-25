using System;
using B2B.Shared.Dto.Base;
using B2B.Shared.Enums;

namespace B2B.Shared.Dto
{
    public class ApplicationDto : DtoBase
    {
        public string InGameName { get; set; }

        public string FormJson { get; set; }

        public int ApplicationFlowId { get; set; }

        public ApplicationStatus Status { get; set; }

        public DateTime StatusCompleteDateUtc { get; set; }
    }
}
