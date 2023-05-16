﻿using AutoMapper;
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

            CreateMap<UserUpdateRequest, User>();

            CreateMap<User, UserUpdateRequest>();

            CreateMap<User, UserCreateResponse>()
                .ForMember(dest => dest.AtCreated, opt => opt.MapFrom(src => src.AtCreated.ToString("dd/MM/yyyy")));

            CreateMap<User, UserSearchResponse>()
                .ForMember(dest => dest.AtCreated, opt => opt.MapFrom(src => src.AtCreated.ToString("dd/MM/yyyy")));

            CreateMap<User, UserClientResponse>();

            CreateMap<ClientCreateRequest, Client>();

            CreateMap<ClientUpdateRequest, Client>();

            CreateMap<Client, ClientCreateResponse>()
                .ForMember(dest => dest.AtCreated, opt => opt.MapFrom(src => src.AtCreated.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Client, ClientSearchResponse>()
                .ForMember(dest => dest.AtCreated, opt => opt.MapFrom(src => src.AtCreated.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.User, opt => opt.Ignore());

            
        }
    }
}
