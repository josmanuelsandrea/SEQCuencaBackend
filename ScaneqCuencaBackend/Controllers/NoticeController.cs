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
    public class NoticeController : ControllerBase
    {
        private readonly NoticeBll _noticeB;
        private readonly IMapper _mapper;
        private readonly SeqcuencabackendContext _db;
        public NoticeController(SeqcuencabackendContext db, IMapper mapper)
        {
            _db = db;
            _noticeB = new NoticeBll(_db);
            _mapper = mapper;
        }
        // GET: api/<NoticeController>
        [HttpGet]
        public List<NoticeResponseModel> GetAll()
        {
            var result = _noticeB.GetAll();
            var mapping = _mapper.Map<List<NoticeResponseModel>>(result);
            return mapping;
        }

        [HttpGet("{id}")]
        public NoticeResponseModel? Get(int id)
        {
            var result = _noticeB.GetById(id);
            var mapping = _mapper.Map<NoticeResponseModel>(result);
            return mapping;
        }

        [HttpGet("vehicle/{id}")]
        public List<NoticeResponseModel> GetByVehicleId(int id)
        {
            var result = _noticeB.GetNoticesByVehicleId(id);
            var mapping = _mapper.Map<List<NoticeResponseModel>>(result);
            return mapping;
        }

        [HttpPost]
        public IActionResult Post(NoticeCreateRequest notice)
        {
            var mapping = _mapper.Map<Notice>(notice);
            var result = _noticeB.Add(mapping);
            if (result == null)
            {
                return BadRequest(new { message = "An error ocurred while saving the notice" });
            }

            return Ok(new { message = "Saved notice"});
        }
    }
}
