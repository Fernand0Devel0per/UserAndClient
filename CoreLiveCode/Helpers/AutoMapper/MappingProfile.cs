using AutoMapper;
using CoreLiveCode.Domain;
using CoreLiveCode.Dtos.Client;
using CoreLiveCode.Dtos.User;

namespace CoreLiveCode.Helpers.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateRequest, User>();
            CreateMap<User, UserCreateResponse>();
            CreateMap<User, UserSearchResponse>();

            CreateMap<ClientCreateRequest, Client>();
            CreateMap<User, ClientCreateResponse>();
            //CreateMap<Client, UserSearchResponse>();
        }
    }
}
