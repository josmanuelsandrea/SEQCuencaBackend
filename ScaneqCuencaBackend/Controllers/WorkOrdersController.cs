using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Params;
using ScaneqCuencaBackend.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrdersController : ControllerBase
    {
        private readonly DbScaniaCuencaContext _db;
        private readonly WorkOrderBll _workOrderB;
        public WorkOrdersController(DbScaniaCuencaContext db)
        {
            _db = db;
            _workOrderB = new WorkOrderBll(db);
        }
        //GET: api/<CustomerController>
        [HttpGet]
        public dynamic GetWorkOrderByCustomerId([FromQuery] int customerId)
        {
            return _workOrderB.getAllWorkOrdersByCustomerId(customerId);
        }

        // GET api/<work_orders>/5
        [HttpGet("{id}")]
        public WorkOrderResponseModel Get(int id)
        {
            return _workOrderB.getWorkOrderById(id);
        }
    }
}

