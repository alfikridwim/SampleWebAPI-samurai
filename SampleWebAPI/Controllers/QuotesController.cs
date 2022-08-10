
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class QuotesController : ControllerBase
    {
        private readonly IQuote _quoteDal;
        public QuotesController(IQuote quoteDal)
        {
            _quoteDal = quoteDal;
        }

        
        [HttpGet]
        public async Task<IEnumerable<QuoteDTO>> Get()
        {
            List<QuoteDTO> quoteDtos = new List<QuoteDTO>();
            var results = await _quoteDal.GetAll();
            foreach(var result in results)
            {
                quoteDtos.Add(new QuoteDTO
                {
                    Id = result.Id,
                    Text = result.Text
                });
            }
            return quoteDtos;
        }
    }
}
