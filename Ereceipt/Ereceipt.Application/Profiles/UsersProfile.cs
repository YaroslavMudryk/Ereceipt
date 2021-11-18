using AutoMapper;
using Ereceipt.Application.ViewModels.Users;
using Ereceipt.Domain.Models;
namespace Ereceipt.Application.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, UserShortViewModel>().ReverseMap();
        }
    }
}