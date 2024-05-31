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
            _busOrderB = new BusOrderBll(db, mapper);
        }
        [HttpGet("customer/{id}")]
        public dynamic GetWorkOrderByCustomerId(int id)
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
    }
}
