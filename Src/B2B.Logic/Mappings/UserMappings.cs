using System;
using AutoMapper;
using B2B.DataAccess.Entities;
using B2B.Logic.Identity;
using B2B.Shared.Dto;

namespace B2B.Logic.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<UserEntity, AppIdentityUser>()
                .ForMember(d => d.LockoutEnd, m => m.MapFrom(s => s.LockoutEnd));
            CreateMap<AppIdentityUser, UserEntity>()
                .ForMember(d => d.LockoutEnd,
                    m => m.MapFrom(s =>
                        s.LockoutEnd.HasValue ? (DateTime?) DateTime.Parse(s.LockoutEnd.ToString()) : null));

            CreateMap<AppIdentityUser, UserDto>()
                .ForMember(d => d.LockoutEnd,
                    m => m.MapFrom(s =>
                        s.LockoutEnd.HasValue ? (DateTime?) DateTime.Parse(s.LockoutEnd.ToString()) : null));
            CreateMap<UserDto, AppIdentityUser>()
                .ForMember(d => d.LockoutEnd, m => m.MapFrom(s => s.LockoutEnd));

            CreateMap<UserClaimEntity, AppIdentityUserClaim>().ReverseMap();

            CreateMap<UserClaimEntity, UserClaimDto>()
                .ForMember(dto => dto.UserId, m => m.MapFrom(e => e.User.Id));

            CreateMap<UserClaimDto, UserClaimEntity>()
                .ForMember(e => e.User, m => m.Ignore());

            CreateMap<AppIdentityUserClaim, UserClaimDto>().ReverseMap();

            CreateMap<UserEntity, UserDto>().ReverseMap();
        }
    }
}
