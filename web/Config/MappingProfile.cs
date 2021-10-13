using api.Models;
using api.Models.DTO;
using AutoMapper;

namespace api.Config
{
    public class MappingProfile : Profile
    {
        /// <summary>Prepare the mapping of Models to DTO Models
        /// for sending data as API request and response</summary>
        public MappingProfile()
        {
            CreateMap<UserDevice, UserDeviceDTO>();

            CreateMap<UserDeviceDTO, Device>()
                .ForMember(dest =>
                    dest.UserDeviceID,
                    opt => opt.MapFrom(src => src.UserDeviceID));
        }
    }
}