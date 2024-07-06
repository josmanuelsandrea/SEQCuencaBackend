using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Interfaces;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckOrderController : ControllerBase, IOrderController
    {
        private readonly SeqcuencabackendContext _db;
        private readonly TruckOrderBll _truckOrderB;
        private readonly IMapper _mapper;
        public TruckOrderController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _truckOrderB = new TruckOrderBll(db, mapper);
            _mapper = mapper;
        }

        [HttpGet]
        public List<WorkOrderResponseModel> GetWorkOrders()
        {
            return _truckOrderB.GetAll();
        }

        [HttpGet("customer/{id}")]
        public List<WorkOrderResponseModel> GetWorkOrderByCustomerId(int id)
        {
            return _truckOrderB.GetAllWorkOrdersByCustomerId(id);
        }

        // GET: api/<BusOrderController>
        [HttpGet("{id}")]
        public WorkOrderResponseModel GetOrderById(int id)
        {
            return _truckOrderB.GetWorkOrderById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] WorkOrderRequestModel data)
        {
            var result = _truckOrderB.CreateWorkOrder(data);
            if (result == null)
            {
                return BadRequest(new { message = "Something wrong happened in the server" });
            }

            return Ok(new { message = "Work order registered succesfully" });
        }
        [HttpPost("range")]
        public List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromBody] WorkOrderRange range)
        {
            var results = _truckOrderB.GetWorkOrdersByFid(range);
            return results;
        }

        [HttpPost("dates")]
        public List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromBody] WorkOrderDate range)
        {
            var results = _truckOrderB.GetWorkOrdersByDateRange(range);
            return results;
        }

        [HttpPut]
        public IActionResult Put([FromBody] WorkOrderEditRequestModel data)
        {
            var result = _truckOrderB.EditWorkOrder(data);
            if (result == null)
            {
                return BadRequest(new { message = "Something wrong happened in the server" });
            }
            return Ok(new { message = "Work order was edited succesfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _truckOrderB.DeleteWorkOrder(id);

            if (result == null)
            {
                return BadRequest(new { message = "Something wrong happened in the server " });
            }

            return Ok(new { message = "Work order " + id + " was deleted succesfully" });
        }
    }
}

