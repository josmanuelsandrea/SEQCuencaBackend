using AutoMapper;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;

namespace ScaneqCuencaBackend.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VehicleCreateRequest, Vehicle>();
            CreateMap<WorkOrderRequestModel, BusOrder>();
            CreateMap<WorkOrderRequestModel, TruckOrder>();
            // Add other mappings here
        }
    }
}
