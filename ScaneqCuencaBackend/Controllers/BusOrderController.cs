using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.Helpers;
using ScaneqCuencaBackend.Interfaces;
using ScaneqCuencaBackend.Models;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/WorkOrders")]
    [ApiController]
    // Prueba
    public class BusOrderController : ControllerBase, IOrderController
    {
        private readonly BusOrderBll _busOrderB;
        private readonly IMapper _mapper;
        public BusOrderController(IMapper mapper, BusOrderBll busOrderB)
        {
            _mapper = mapper;
            _busOrderB = busOrderB;
        }
        [HttpGet]
        public ActionResult<ApiResponse<List<WorkOrderResponseModel?>>> GetWorkOrders([FromQuery] string vehicleType)
        {
            if (vehicleType == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new List<WorkOrderResponseModel>());
            }

            var result = _busOrderB.GetOrders(vehicleType);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("warranty")]
        public ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWarrantyOrders()
        {
            var result = _busOrderB.GetWarrantyOrders();
            return StatusCode((int)result.StatusCode, result);
        }
        [HttpGet("vehicle/{id}")]
        public ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWorkOrderByVehicleId(int id)
        {
            var result = _busOrderB.GetWorkOrderByVehicleId(id);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("customer/{id}")]
        public ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWorkOrderByCustomerId(int id)
        {
            var result = _busOrderB.GetAllWorkOrdersByCustomerId(id);
            return StatusCode((int)result.StatusCode, result);
        }

        // GET: api/<BusOrderController>
        [HttpGet("{id}")]
        public ActionResult<ApiResponse<WorkOrderResponseModel>> GetOrderByFid(int id)
        {
            var result = _busOrderB.GetWorkOrderByFid(id);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public ActionResult<ApiResponse<WorkOrderResponseModel>> Post([FromBody] WorkOrderRequestModel data)
        {
            var result = _busOrderB.CreateWorkOrder(data);
            if (result.Data == null)
            {
                return StatusCode((int)result.StatusCode, result);
            }

            var WorkOrderResponse = _mapper.Map<WorkOrderResponseModel>(result.Data);

            return StatusCode((int)result.StatusCode, new ApiResponse<WorkOrderResponseModel>(WorkOrderResponse, result.Message, HttpStatusCode.OK));
        }

        [HttpPost("range")]
        public ActionResult<ApiResponse<List<WorkOrderResponseModel>>> GetWorkOrdersByRangeNumber([FromQuery] string vehicleType, [FromBody] WorkOrderRange range)
        {
            if (vehicleType == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new List<WorkOrderResponseModel>());
            }
            var result = _busOrderB.GetWorkOrdersByFid(vehicleType, range);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("dates")]
        public ActionResult<List<WorkOrderResponseModel>> GetWorkOrdersByRangeDate([FromQuery] string vehicleType, [FromBody] WorkOrderDate range)
        {
            if (vehicleType == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, new List<WorkOrderResponseModel>());
            }
            var result = _busOrderB.GetWorkOrdersByDateRange(vehicleType, range);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut]
        public ActionResult<ApiResponse<WorkOrderResponseModel>> Update([FromBody] WorkOrderEditRequestModel data)
        {
            var result = _busOrderB.EditWorkOrder(data);

            if (result.Data == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, result);
            }

            var mappingResponse = _mapper.Map<WorkOrderResponseModel>(result.Data);
            return StatusCode((int)result.StatusCode, new ApiResponse<WorkOrderResponseModel>(mappingResponse, result.Message, HttpStatusCode.OK));
        }

        [HttpDelete("{id}")]
        public ActionResult<ApiResponse<int?>> Delete(int id)
        {
            var result = _busOrderB.DeleteWorkOrder(id);

            if (result.Data == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound, result);
            }

            return StatusCode((int)result.StatusCode, result);
        }
    }
}
