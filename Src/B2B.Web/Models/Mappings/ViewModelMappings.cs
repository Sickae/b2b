using AutoMapper;
using B2B.Shared.Dto;

namespace B2B.Web.Models.Mappings
{
    public class ViewModelMappings : Profile
    {
        public ViewModelMappings()
        {
            CreateMap<ApplicationFlowDto, ApplicationViewModel>().ReverseMap();
        }
    }
}
