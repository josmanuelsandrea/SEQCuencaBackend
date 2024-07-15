using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/Maintenance")]
    [ApiController]
    public class MaintenancesRegistryController : ControllerBase
    {
        private readonly SeqcuencabackendContext _db;
        private readonly MaintenanceRegistryBll _maintenanceRegistryBll;
        private readonly IMapper _mapper;
        public MaintenancesRegistryController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _maintenanceRegistryBll = new(db);
            _mapper = mapper;
        }

        [HttpGet("order/{id}")]
        public List<MaintenanceRegistryResponse> GetRegistryByOrder(int id)
        {
            var result = _maintenanceRegistryBll.GetByOrderId(id);
            var mapResult = _mapper.Map<List<MaintenanceRegistryResponse>>(result);
            return mapResult;
        }

        [HttpGet("vehicle/{id}")]
        public List<MaintenanceRegistryResponse> GetRegistryByVehicle(int id)
        {
            var result = _maintenanceRegistryBll.GetByVehicleId(id);
            var mapResult = _mapper.Map<List<MaintenanceRegistryResponse>>(result);
            return mapResult;
        }
    }
}
