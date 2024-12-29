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
        private readonly TrackingBll _trackingBll;

        public TrackingController(TrackingBll trackingBll)
        {
            _trackingBll = trackingBll;
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

        [HttpGet("Warnings")]
        public List<NoticeResponseModel> GetPendingWarnings()
        {
            List<NoticeResponseModel> response = _trackingBll.GetPendingWarnings();
            return response;
        }
    }
}
