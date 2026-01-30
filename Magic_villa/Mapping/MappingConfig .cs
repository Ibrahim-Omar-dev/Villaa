using AutoMapper;
using Magic_villa.Model;
using Magic_villa.Model.Dto.VillaDto;
using Magic_villa.Model.Dto.VillaNumberDto;


namespace Magic_villa.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDto>().ReverseMap();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();

            CreateMap<VillaNumber,VillaNumberDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumber, VillaNumberUpdateDto>().ReverseMap();

            CreateMap<AppUser, UserDto>().ReverseMap();
        }
    }
}
