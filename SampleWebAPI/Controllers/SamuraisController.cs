using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;
using SampleWebAPI.Models;

namespace SampleWebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SamuraisController : ControllerBase
    {
        private readonly ISamurai _samuraiDAL;
        private readonly IMapper _mapper;
        public SamuraisController(ISamurai samuraiDAL,IMapper mapper)
        {
            _samuraiDAL = samuraiDAL;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<SamuraiReadDTO>> Get()
        {
            //List<SamuraiReadDTO> samuraiDTO = new List<SamuraiReadDTO>();
            /*foreach (var result in results)
           {
               samuraiDTO.Add(new SamuraiReadDTO
               {
                   Id = result.Id,
                   Name = result.Name
               });
           }*/
            var results = await _samuraiDAL.GetAll();
            var samuraiDTO = _mapper.Map<IEnumerable<SamuraiReadDTO>>(results);
           
            return samuraiDTO;
        }

        [HttpGet("{id}")]
        public async Task<SamuraiReadDTO> Get(int id)
        {
            /*SamuraiReadDTO samuraiDTO = new SamuraiReadDTO();
            samuraiDTO.Id = result.Id;
            samuraiDTO.Name = result.Name;*/
            var result = await _samuraiDAL.GetById(id);
            if (result == null) throw new Exception($"data {id} tidak ditemukan");
            var samuraiDTO = _mapper.Map<SamuraiReadDTO>(result);
           
            return samuraiDTO;
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<SamuraiReadDTO>> GetByName(string name)
        {
            List<SamuraiReadDTO> samuraiDtos = new List<SamuraiReadDTO>();
            var results = await _samuraiDAL.GetByName(name);
            foreach(var result in results)
            {
                samuraiDtos.Add(new SamuraiReadDTO
                {
                    Id = result.Id,
                    Name = result.Name
                });
            }
            return samuraiDtos;
        }

        [HttpGet("WithQuotes")]
        public async Task<IEnumerable<SamuraiWithQuotesDTO>> GetSamuraiWithQuote()
        {
            var results = await _samuraiDAL.GetSamuraiWithQuotes();
            var samuraiWithQuoteDtos = _mapper.Map<IEnumerable<SamuraiWithQuotesDTO>>(results);  
            return samuraiWithQuoteDtos;
        }
        [HttpGet("WithSwords")]
        public async Task<IEnumerable<SamuraiWithSwordsDTO>> GetSamuraiWithSwords()
        {

            var results = await _samuraiDAL.GetSamuraiWithSwords();
            var samuraiWithSwordDtos = _mapper.Map<IEnumerable<SamuraiWithSwordsDTO>>(results);
            return samuraiWithSwordDtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post(SamuraiCreateDTO samuraiCreateDto)
        {
            try
            {
                var newSamurai = _mapper.Map<Samurai>(samuraiCreateDto);
                var result = await _samuraiDAL.Insert(newSamurai);
                var samuraiReadDto = _mapper.Map<SamuraiReadDTO>(result);
                
                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("WithSwords")]
        


        [HttpPut]
        public async Task<ActionResult> Put(SamuraiReadDTO samuraiDto)
        {
            try
            {
                var updateSamurai = new Samurai
                {
                    Id = samuraiDto.Id,
                    Name = samuraiDto.Name
                };
                var result = await _samuraiDAL.Update(updateSamurai);
                return Ok(samuraiDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("WithSwords")]
        public async Task<ActionResult> InsertSamuraiWSword(SamuraiCreateWithSwordsDTO samuraiCreateWithSwordsDTO)
        {
            try
            {
                var newSamurai = _mapper.Map<Samurai>(samuraiCreateWithSwordsDTO);
                var result = await _samuraiDAL.Insert(newSamurai);
                var samuraiReadDto = _mapper.Map<SamuraiReadDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _samuraiDAL.Delete(id);
                return Ok($"Data samurai dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
