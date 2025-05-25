using AutoMapper;
using FinancialBuddy.Application.DTOs.User;
using FinancialBuddy.Domain.Entities;


namespace FinancialBuddy.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserRequest, User>();
            CreateMap<UpdateUserRequest, User>();
        }
    }
}
