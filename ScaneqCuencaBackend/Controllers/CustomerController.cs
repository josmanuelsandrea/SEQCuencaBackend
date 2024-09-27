using AutoMapper;
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
        private readonly SeqcuencabackendContext _db;
        private readonly CustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerController(SeqcuencabackendContext context, IMapper mapper)
        {
            _db = context;
            _customerB = new CustomerBll(context, mapper);
            _repository = new CustomerRepository(context);
            _mapper = mapper;
        }
        //GET: api/<CustomerController>
        [HttpGet]
        public List<CustomerResponseModel> Get()
        {
            var result = _customerB.getAllCustomers();
            var response = _mapper.Map<List<CustomerResponseModel>>(result);
            return response;
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

        [HttpGet("vehicles/{id}")]
        public List<VehicleResponse> GetVehicles(int id)
        {
            var data = _customerB.getCustomerVehicles(id);
            var response = _mapper.Map<List<VehicleResponse>>(data);
            return response;
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerRequestModel model)
        {
            var createdUser = _repository.CreateCustomer(model);
            if (createdUser == null)
            {
                BadRequest(new
                {
                    response = "An error ocurred.",
                    responseCode = 500
                });
            }

            return Ok(new
            {
                response = "Customer created successfully",
                responseCode = 200
            });
        }
    }
}
