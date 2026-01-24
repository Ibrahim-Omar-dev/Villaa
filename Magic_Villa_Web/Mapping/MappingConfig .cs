using AutoMapper;
using Magic_Villa_Web.Models.Dto.VillaDto;
using Magic_Villa_Web.Models.Dto.VillaNumberDto;

namespace Magic_Villa_Web.Mapping
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDto, VillaCreateDto>().ReverseMap();
            CreateMap<VillaDto,VillaUpdateDto>().ReverseMap();

            CreateMap<VillaNumberDto,VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberUpdateDto>().ReverseMap();
        }
    }
}
