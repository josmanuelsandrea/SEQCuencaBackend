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
    public class TrackingController : ControllerBase
    {
        private readonly SeqcuencabackendContext _db;
        private readonly IMapper _mapper;
        private readonly TrackingBll _trackingBll;

        public TrackingController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _trackingBll = new(db, mapper);
        }
        // GET: api/<TrackingController>
        [HttpGet]
        public List<TrackingDataResponse> GetTrackingData()
        {
            return _trackingBll.GetTrackingData();
        }

        [HttpGet("Kilometers")]
        public VehicleKilometerRangeResponse? GetVehiclesByKilometersRange([FromQuery] VehiclesKilometerRangeModel ranges)
        {
            if (ranges == null)
            {
                return null;
            }

            return _trackingBll.GetVehicleKilometerRange(ranges);
        }
    }
}
