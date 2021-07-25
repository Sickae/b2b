using AutoMapper;
using B2B.Shared.Dto;

namespace B2B.Web.Models.Mappings
{
    public class ViewModelMappings : Profile
    {
        public ViewModelMappings()
        {
            CreateMap<ApplicationFlowDto, ApplicationViewModel>();
            CreateMap<ApplicationViewModel, ApplicationDto>()
                .ForMember(x => x.ApplicationFlowId, m => m.MapFrom(x => x.Id))
                .ForMember(x => x.Id, m => m.Ignore());
        }
    }
}
