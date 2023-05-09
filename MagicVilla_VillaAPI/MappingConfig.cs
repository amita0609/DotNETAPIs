using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap <Villa,VillaDTO> ();
            CreateMap<VillaDTO, Villa>();
            CreateMap<Villa, VillaCreateDTO>();
            CreateMap<VillaCreateDTO, Villa>();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();


            CreateMap<VillaDTO, VillaNumber>().ReverseMap();


            CreateMap<VillaNumber, VillaNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();

            CreateMap<ApplicationUser, UserDTO>().ReverseMap();

        }
    }
}
