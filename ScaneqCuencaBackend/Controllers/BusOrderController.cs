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
    public class BusOrderController : ControllerBase
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
        public List<WorkOrderResponseModel> GetWorkOrders()
        {
            return _busOrderB.GetAll();
        }

        [HttpGet("customer/{id}")]
        public List<WorkOrderResponseModel> GetWorkOrderByCustomerId(int id)
        {
            return _busOrderB.getAllWorkOrdersByCustomerId(id);
        }

        // GET: api/<BusOrderController>
        [HttpGet("{id}")]
        public WorkOrderResponseModel Get(int id)
        {
            return _busOrderB.getWorkOrderById(id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] WorkOrderRequestModel data)
        {
            var result = _busOrderB.createWorkOrder(data);
            if (result == null)
            {
                return BadRequest(new { message = "Something wrong happened in the server" });
            }

            return Ok(new { message = "Work order registered succesfully" });
        }
        [HttpPost("range")]
        public List<WorkOrderResponseModel> GetWorkOrdersByRangeNumber([FromBody] WorkOrderRange range)
        {
            var results = _busOrderB.GetWorkOrdersByFid(range);
            return results;
        }

        [HttpPut]
        public IActionResult Put([FromBody] WorkOrderEditRequestModel data)
        {
            var result = _busOrderB.EditWorkOrder(data);
            if (result == null)
            {
                return BadRequest(new { message = "Something wrong happened in the server" });
            }
            return Ok(new { message = "Work order was edited succesfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _busOrderB.DeleteWorkOrder(id);

            if (result == null)
            {
                return BadRequest(new { message = "Something wrong happened in the server " });
            }

            return Ok(new { message = "Work order " + id + " was deleted succesfully" });
        }
    }
}
