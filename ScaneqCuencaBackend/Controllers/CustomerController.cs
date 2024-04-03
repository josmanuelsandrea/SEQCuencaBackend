using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBll CustomerB;
        private readonly DbScaniaCuencaContext _db;
        public CustomerController(DbScaniaCuencaContext context)
        {
            _db = context;
            CustomerB = new CustomerBll(context);
        }
        //GET: api/<CustomerController>
        [HttpGet]
        public List<CustomerResponseModel> Get()
        {
            return CustomerB.getAllCustomers();
        }
    }
}
