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
        //GET: api/<CustomerController>
        [HttpGet]
        public dynamic GetWorkOrderByCustomerId([FromQuery] int customerId)
        {
            return WorkOrderBll.getAllWorkOrdersByCustomerId(customerId);
        }

        // GET api/<work_orders>/5
        [HttpGet("{id}")]
        public WorkOrderResponseModel Get(int id)
        {
            return WorkOrderBll.getWorkOrderById(id);
        }

        // POST api/<work_orders>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<work_orders>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<work_orders>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
