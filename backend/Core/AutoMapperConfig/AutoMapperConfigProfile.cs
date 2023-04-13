using AutoMapper;
using backend.Core.Dtos.Owner;
using backend.Core.Dtos.Vehicle;
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
            CreateMap<VehicleCreateDto, Vehicle>();
            CreateMap<Vehicle, VehicleGetDto>()
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => $"{src.Owner.FirstName} {src.Owner.LastName}"));
        }
    }
}
