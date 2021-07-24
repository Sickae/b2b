using B2B.Shared.Dto.ApplicationFlow;
using B2B.Shared.Dto.Base;

namespace B2B.Shared.Dto
{
    public class ApplicationFlowDto : DtoBase
    {
        public string Description { get; set; }

        public ApplicationFlowDescription FlowDescription { get; set; }
    }
}
