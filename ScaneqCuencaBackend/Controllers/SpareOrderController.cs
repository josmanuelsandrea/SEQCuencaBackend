using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ScaneqCuencaBackend.Bll;
using ScaneqCuencaBackend.DBModels;
using ScaneqCuencaBackend.Models.RequestModels;
using ScaneqCuencaBackend.Models.ResponseModels;
using ScaneqCuencaBackend.Repository;
using ScaneqCuencaBackend.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ScaneqCuencaBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpareOrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly SparePartRepository _sparePartRepository;
        private readonly SpareOrderRepository _spareOrderRepository;
        private readonly SpareOrderBll _spareOrderB;
        private readonly PdfService _pdfService;
        private readonly PDFDataBll _pdfDataBll;
        public SpareOrderController(IMapper mapper, PdfService pdfService, PDFDataBll pdfDataBll, SpareOrderBll spareOrderB, SparePartRepository sparePartRepository, SpareOrderRepository spareOrderRepository)
        {
            _mapper = mapper;
            _pdfService = pdfService;
            _pdfDataBll = pdfDataBll;
            _spareOrderB = spareOrderB;
            _sparePartRepository = sparePartRepository;
            _spareOrderRepository = spareOrderRepository;
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

        [HttpGet("downloadWorkOrder")]
        public async Task<IActionResult> DownloadWorkOrder([FromQuery] int id)
        {
            string url = "http://127.0.0.1:5050/generate-pdf/";
            var POSTData = _pdfDataBll.PDFWorkOrderData(id);

            if (POSTData != null)
            {   
                var pdfStream = await _pdfService.PostDataAsync(url, POSTData);
                return File(pdfStream, "application/pdf", $"{POSTData.Data.WorkOrderId}.pdf");
            } else
            {
                return BadRequest();
            }
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

        [HttpPost("substract")]
        public IActionResult SubstractItemToOrder(SpareRegisterRequest spareRegisterRequest)
        {
            var result = _spareOrderB.SubstractItemToOrder(spareRegisterRequest, 1);
            if (result == null)
            {
                return BadRequest(new { message = "Ha ocurrido un error al añadir el item a la lista" });
            }

            return Ok(result);
        }

        [HttpPost("delete")]
        public IActionResult DeleteItemToOrder(SpareRegisterRequest spareRegisterRequest)
        {
            var result = _spareOrderB.SubstractItemToOrder(spareRegisterRequest, 999999);
            if (result == null)
            {
                return BadRequest(new { message = "Ha ocurrido un error al añadir el item a la lista" });
            }

            return Ok(result);
        }

        [HttpPost("createSpare")]
        public IActionResult CreateSparePart(SpareRequest spareData)
        {
            var result = _spareOrderB.CreateSparePart(spareData);
            if (result == null)
            {
                return BadRequest(new { message = "Ha ocurrido un error al momento de crear el repuesto" });
            }

            return Ok(result);
        }
    }
}
