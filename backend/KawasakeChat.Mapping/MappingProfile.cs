using AutoMapper;
using KawasakeChat.Models.Requests;
using KawasakeChat.Domain.Entities;
using KawasakeChat.Dto.GroupChat;
using KawasakeChat.Dto.UserAccount;

namespace KawasakeChat.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User account
        CreateMap<Useraccount, UserAccountDto>().ReverseMap();
        
        // Group chat
        CreateMap<Groupchat, GroupChatDto>().ReverseMap();
    }
}