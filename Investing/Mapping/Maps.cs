using AutoMapper;
using Investing.Models;
using Investing.Models.DTO;
using System.Linq;

namespace Investing.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
          CreateMap<User, UserDTO>();

        
            CreateMap<Credentials, CredentialsDTO>();

            CreateMap<User, UserCredentialsDTO>()
           .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Credentials.Login))
           .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Credentials.Password));
        }    
    }
}
