using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerBll _customerB;
        private readonly DbScaniaCuencaContext _db;
        private readonly CustomerRepository _repository;
        public CustomerController(DbScaniaCuencaContext context)
        {
            _db = context;
            _customerB = new CustomerBll(context);
            _repository = new CustomerRepository(context);
        }
        //GET: api/<CustomerController>
        [HttpGet]
        public List<CustomerResponseModel> Get()
        {
            return _customerB.getAllCustomers();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Customer? foundUser = _repository.getCustomerById(id);

            if (foundUser == null)
            {
                return NotFound();
            }

            CustomerResponseModel response = new()
            {
                Id = foundUser.Id,
                Name = foundUser.Name
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerRequestModel model)
        {
            var createdUser = _repository.CreateCustomer(model);
            if (createdUser == null)
            {
                BadRequest();
            }

            return Ok(new
            {
                response = "Customer created successfully"
            });
        }
    }
}
