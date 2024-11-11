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
    public class SpareOrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SeqcuencabackendContext _db;
        private readonly SparePartRepository _sparePartRepository;
        private readonly SpareOrderRepository _spareOrderRepository;
        private readonly SpareOrderBll _spareOrderB;
        public SpareOrderController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _sparePartRepository = new(db);
            _spareOrderB = new(db, mapper);
            _spareOrderRepository = new(db);
        }

        [HttpGet("openOrders")]
        public List<SpareOrderResponseModel> GetOpenOrders()
        {
            return _spareOrderB.GetOpenOrders();
        }

        [HttpGet("closedOrders")]
        public List<SpareOrderResponseModel> GetClosedOrders()
        {
            return _spareOrderB.GetClosedOrders();
        }
        // GET: api/<SpareOrderController>
        [HttpGet("spare")]
        public SparePartResponseModel GetSpareByCode([FromQuery] string code)
        {
            var result = _sparePartRepository.GetByCode(code);
            var map = _mapper.Map<SparePartResponseModel>(result);
            return map;
        }

        [HttpPut("closeOrder")]
        public IActionResult CloseOrder([FromQuery] int spareId)
        {
            var result = _spareOrderB.CloseOrder(spareId);
            if (result != null)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("spareOrder")]
        public SpareOrderResponseModel? GetSpareOrderById([FromQuery] int id)
        {
            var result = _spareOrderRepository.GetById(id);
            var map = _mapper.Map<SpareOrderResponseModel>(result);

            return map;
        }

        [HttpPost("order")]
        public IActionResult CreateOrder(SpareOrderRequest spareOrderRequest)
        {
            var result = _spareOrderB.CreateOrder(spareOrderRequest);
            if (result == null)
            {
                return BadRequest(new { message = "Hubo un error al crear la orden" });
            }

            return Ok(result);
        }
        [HttpPost("add")]
        public IActionResult AddItemToOrder(SpareRegisterRequest spareRegisterRequest)
        {
            var result = _spareOrderB.AddItemToOrder(spareRegisterRequest);
            if (result == null)
            {
                return BadRequest(new { message = "Ha ocurrido un error al añadir el item a la lista" });
            }

            return Ok(result);
        }
    }
}
