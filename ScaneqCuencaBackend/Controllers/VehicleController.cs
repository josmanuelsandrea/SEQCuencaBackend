using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleBll _vehicleB;
        private readonly SeqcuencabackendContext _db;
        private readonly IMapper _mapper;

        public VehicleController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _vehicleB = new VehicleBll(_db, mapper);
            _mapper = mapper;
        }
        [HttpPost("{id}")]
        public List<WorkOrderResponseModel> GetVehicles(int id, [FromBody] VehicleTypeRequest data)
        {
            return _vehicleB.getWorkOrderByVehicleId(id, data.type);
        }

        [HttpPost]
        public Vehicle? CreateVehicle([FromBody] VehicleCreateRequest data)
        {
            return _vehicleB.createVehicle(data, _mapper);
        }
    }
}
