using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TruckOrderController : ControllerBase
    {
        private readonly SeqcuencabackendContext _db;
        private readonly TruckOrderBll _truckOrderB;
        private readonly IMapper _mapper;
        public TruckOrderController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _truckOrderB = new TruckOrderBll(db, mapper);
        }
        //GET: api/<CustomerController>
        [HttpGet("customer/{id}")]
        public dynamic GetWorkOrderByCustomerId(int customerId)
        {
            return _truckOrderB.getAllWorkOrdersByCustomerId(customerId);
        }

        // GET api/<work_orders>/5
        [HttpGet("{id}")]
        public WorkOrderResponseModel Get(int id)
        {
            return _truckOrderB.getWorkOrderById(id);
        }
    }
}

