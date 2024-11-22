using AutoMapper;
using BLL.Dtos.Users;
using Learning_System.EndPoint.Models.Auth;

namespace Learning_System.EndPoint.Mapper
{
	public class AuthMappingProfile : Profile
	{
        public AuthMappingProfile()
        {
            CreateMap<RegisterViewModel, PersonDto>();
                
        }
    }
}
