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
        private readonly IMapper _mapper;

        public VehicleController(VehicleBll vehicleB, IMapper mapper)
        {
            _vehicleB = vehicleB;
            _mapper = mapper;
        }

        [HttpGet("cooperative/{cooperativeID}")]
        public List<VehicleResponse> GetByCooperative(int cooperativeID)
        {
            var result = _vehicleB.GetVehicleByCooperative(cooperativeID);
            var mapping = _mapper.Map<List<VehicleResponse>>(result);
            return mapping;
        }

        [HttpPost]
        public Vehicle? CreateVehicle([FromBody] VehicleCreateRequest data)
        {
            return _vehicleB.createVehicle(data);
        }

        [HttpPut]
        public IActionResult EditVehicle([FromBody] VehicleEditRequest data)
        {
            var mapResult = _mapper.Map<Vehicle>(data);
            var result = _vehicleB.EditVehicle(mapResult);

            if (result == null)
            {
                return BadRequest(new { message = "An error ocurred when trying to edit the vehicle" });
            }

            return Ok(new { message = "Edited vehicle succesfully" });
        }
    }
}
