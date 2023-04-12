using AutoMapper;
using backend.Core.Dtos.Owner;
using backend.Core.Entities;

namespace backend.Core.AutoMapperConfig
{
    public class AutoMapperConfigProfile : Profile
    {
        public AutoMapperConfigProfile() {

            // Owner
            CreateMap<OwnerCreateDto, Owner>();
            CreateMap<Owner, OwnerGetDto>();

            // Vehicle
        }
    }
}
