using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Shared.Dto;
using B2B.Shared.Dto.ApplicationFlow;
using Newtonsoft.Json;

namespace B2B.Logic.Mappings
{
    public class ApplicationFlowMappings : Profile
    {
        public ApplicationFlowMappings()
        {
            CreateMap<ApplicationFlowEntity, ApplicationFlowDto>()
                .ForMember(x => x.Description,
                    m => m.MapFrom(x => JsonConvert.DeserializeObject<ApplicationFlowDescription>(x.DescriptionJson)));

            CreateMap<ApplicationFlowDto, ApplicationFlowEntity>()
                .ForMember(x => x.DescriptionJson, m => m.MapFrom(x => JsonConvert.SerializeObject(x.Description)));
        }
    }
}
