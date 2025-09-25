using AutoMapper;
using EletronicPoint.Application.DTOs.User;
using EletronicPoint.Domain.Entities;

namespace EletronicPoint.Application.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserResponse>();
            CreateMap<UserResponse, User>();
        }
    }
}
