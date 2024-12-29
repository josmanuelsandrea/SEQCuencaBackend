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
    public class CooperativeController : ControllerBase
    {
        private readonly CooperativeBll _cooperativeB;
        public CooperativeController(CooperativeBll cooperativeB)
        {
            _cooperativeB = cooperativeB;
        }
        // GET: api/<CooperativeController>
        [HttpGet]
        public List<CooperativeResponseModel> Get()
        {
            return _cooperativeB.GetAllCooperatives();
        }

        // GET api/<CooperativeController>/5
        [HttpGet("{id}")]
        public CooperativeResponseModel Get(int id)
        {
            return _cooperativeB.GetCooperativeByID(id);
        }
    }
}
