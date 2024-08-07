using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Helpers;
using ScaneqCuencaBackend.Interfaces;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/WorkOrders")]
    [ApiController]
    public class BusOrderController : ControllerBase, IOrderController
    {
        private readonly SeqcuencabackendContext _db;
        private readonly BusOrderBll _busOrderB;
        private readonly IMapper _mapper;
        public BusOrderController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _busOrderB = new BusOrderBll(db, mapper);
        }
        [HttpGet]
        public List<WorkOrderResponseModel> GetWorkOrders([FromQuery] string vehicleType)
        {
            if (vehicleType == null)
            {
                return new List<WorkOrderResponseModel>();
            }
            return _busOrderB.GetOrders(vehicleType);
        }

        [HttpGet("warranty")]
        public List<WorkOrderResponseModel> GetWarrantyOrders()
        {
            return _busOrderB.GetWarrantyOrders();
        }
        [HttpGet("vehicle/{id}")]
        public List<WorkOrderResponseModel> GetWorkOrderByVehicleId(int id)
        {
            return _busOrderB.GetWorkOrderByVehicleId(id);
        }

        [HttpGet("customer/{id}")]
        public List<WorkOrderResponseModel> GetWorkOrderByCustomerId(int id)
        {
            return _busOrderB.GetAllWorkOrdersByCustomerId(id);
        }

        // GET: api/<BusOrderController>
        [HttpGet("{id}")]
        public ActionResult<GenericResponse<WorkOrderResponseModel>> GetOrderById(int id)
        {
            var result = _busOrderB.GetWorkOrderById(id);
            var WorkOrderResponse = _mapper.Map<WorkOrderResponseModel>(result);
            GenericResponse<WorkOrderResponseModel> response = new()
            {
                Code = 200,
                Message = ResponseMessages.SUCCESS,
                Model = WorkOrderResponse
            };

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<GenericResponse<WorkOrderResponseModel>> Post([FromBody] WorkOrderRequestModel data)
        {
            var result = _busOrderB.CreateWorkOrder(data);
            var WorkOrderResponse = _mapper.Map<WorkOrderResponseModel>(result);

            if (result == null)
            {
                return BadRequest(new GenericResponse<WorkOrderResponseModel>()
                {
                    Code = 400,
                    Message = ResponseMessages.UNKNOWN_ERROR,
                    Model = WorkOrderResponse
                });
            }

            return Ok(new GenericResponse<WorkOrderResponseModel>()
            {
                Code = 200,
                Model = WorkOrderResponse,
                Message = ResponseMessages.SUCCESS
            });
        }
        [HttpPost("range")]
        public List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromQuery] string vehicleType, [FromBody] WorkOrderRange range)
        {
            if (vehicleType == null)
            {
                return new List<WorkOrderResponseModel>();
            }
            var results = _busOrderB.GetWorkOrdersByFid(vehicleType, range);
            return results;
        }

        [HttpPost("dates")]
        public List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromQuery] string vehicleType, [FromBody] WorkOrderDate range)
        {
            if (vehicleType == null)
            {
                return new List<WorkOrderResponseModel>();
            }
            var results = _busOrderB.GetWorkOrdersByDateRange(vehicleType, range);
            return results;
        }

        [HttpPut]
        public ActionResult<GenericResponse<WorkOrderResponseModel>> Update([FromBody] WorkOrderEditRequestModel data)
        {
            var result = _busOrderB.EditWorkOrder(data);
            var WorkOrderResponse = _mapper.Map<WorkOrderResponseModel>(result);

            if (result == null)
            {
                return BadRequest(new GenericResponse<WorkOrderResponseModel>()
                {
                    Message = ResponseMessages.UNKNOWN_ERROR,
                    Code = 400,
                    Model = WorkOrderResponse
                });
            }

            return Ok(new GenericResponse<WorkOrderResponseModel>()
            {
                Message = ResponseMessages.SUCCESS,
                Code = 200,
                Model = WorkOrderResponse
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<GenericResponse<int?>> Delete(int id)
        {
            var result = _busOrderB.DeleteWorkOrder(id);

            if (result == null)
            {
                return BadRequest(new GenericResponse<int?>()
                {
                    Message = ResponseMessages.UNKNOWN_ERROR,
                    Code = 400,
                    Model = result
                });
            }

            return Ok(new GenericResponse<int?>()
            {
                Message = ResponseMessages.SUCCESS,
                Code = 200,
                Model = result
            });
        }
    }
}
