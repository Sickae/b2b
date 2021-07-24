using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Shared.Dto;

namespace B2B.Logic.Mappings
{
    public class ApplicationMappings : Profile
    {
        public ApplicationMappings()
        {
            CreateMap<ApplicationEntity, ApplicationDto>().ReverseMap();
        }
    }
}
