using AutoMapper;
using KawasakeChat.Application.Models.Requests;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Dto.UserAccount;

namespace KawasakeChat.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserAccountCreateRequest, UserAccountCreateDto>().ReverseMap();
        CreateMap<Useraccount, UserAccountDto>().ReverseMap();
    }
}