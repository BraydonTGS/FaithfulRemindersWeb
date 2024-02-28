using AutoMapper;
using FaithfulRemindersWeb.Business.Login.Dto;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.Mapping
{
    /// <summary>
    /// AutoMapper Mapping Profiles
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<ToDoItem, ToDoItemDto>().ReverseMap();
            CreateMap<Password, PasswordDto>().ReverseMap();
            CreateMap<LoginRequestDto, UserDto>().ReverseMap();
        }
    }
}
