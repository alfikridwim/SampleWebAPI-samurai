using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebAPI.Data.DAL;
using SampleWebAPI.Data;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;
using SampleWebAPI.Helpers;
using SampleWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace SampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SwordsController : ControllerBase
    {
        private readonly ISword _swordDal;
        //private readonly IElement _elementDal;
        private readonly IMapper _mapper;
        private readonly SamuraiContext _context;

        public SwordsController(ISword swordDal, IMapper mapper, SamuraiContext context)
        {
            _swordDal = swordDal;
            _mapper = mapper;
            _context = context;
        }


        [HttpGet]
        public async Task<IEnumerable<SwordReadDTO>> Get()
        {
            List<SwordReadDTO> swordDtos = new List<SwordReadDTO>();
            var results = await _swordDal.GetAll();
            foreach (var result in results)
            {
                swordDtos.Add(new SwordReadDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Weight = result.Weight
                });
            }
            return swordDtos;
        }
        [HttpGet("pagging")]
        public async Task<IEnumerable<SwordReadDTO>> GetAllSword([FromQuery] PaggingParam obj)
        {
            //List<SwordReadDTO> swordDtos = new List<SwordReadDTO>();
            var results = await _swordDal.GetAllSword(obj);
            var swordDtos = _mapper.Map<IEnumerable<SwordReadDTO>>(results);
            /*foreach (var result in results)
            {
                swordDtos.Add(new SwordReadDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Weight = result.Weight
                });
            }*/
            return swordDtos;
        }


        [HttpPost]
        public async Task<ActionResult> Post(SwordCDTO samuraiCreateDto)
        {
            try
            {
                var newSamurai = _mapper.Map<Sword>(samuraiCreateDto);
                var result = await _swordDal.Insert(newSamurai);
                var samuraiReadDto = _mapper.Map<SwordReadDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, samuraiReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("WithType")]
        public async Task<ActionResult> InsertSwordWType(SwordCDTO swordCreateWithType)
        {
            try
            {
                var newSword = _mapper.Map<Sword>(swordCreateWithType);
                var result = await _swordDal.Insert(newSword);
                var ReadDto = _mapper.Map<SwordWithElementTypeDTO>(result);

                return CreatedAtAction("Get", new { id = result.Id }, ReadDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> Put(SwordReadDTO swordDto)
        {
            try
            {
                var updateSword = new Sword
                {
                    Id = swordDto.Id,
                    Name = swordDto.Name,
                    Weight = swordDto.Weight
                };
                var result = await _swordDal.Update(updateSword);
                return Ok(swordDto);
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
                await _swordDal.Delete(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        

        [HttpGet("ByName")]
        public async Task<IEnumerable<SwordReadDTO>> GetByName(string name)
        {
            List<SwordReadDTO> swordDtos = new List<SwordReadDTO>();
            var results = await _swordDal.GetByName(name);
            foreach (var result in results)
            {
                swordDtos.Add(new SwordReadDTO
                {
                    Id = result.Id,
                    Name = result.Name,
                    Weight = result.Weight
                });
            }
            return swordDtos;
        }

        [HttpPut("ElementToExistingSword")]
        public async Task Put(AddSwordElementDTO obj)
        {
            //var newSword = _mapper.Map<Sword>(swordCreateWithType);
            try
            {
                var sword = await _context.Swords.Include(e => e.Elements).FirstOrDefaultAsync(e => e.Id == obj.SwordId);
                var element = await _context.Elements.FirstOrDefaultAsync(c => c.ElementId == obj.ElementId);
                sword.Elements.Add(element);
                await _context.SaveChangesAsync();
                /*var updateSE = new Sword
                {
                    Id = obj.SwordId,
                    Elements = new List<Element>
                    {

                        new Element { ElementId = obj.ElementId }
                    }
                };

                var result = await _swordDal.AddElementToExistingSword(updateSE);
                return Ok(obj);*/
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
        }
        [HttpDelete("Element")]
        public async Task<ActionResult> DelElementToExistingSword(int id)
        {
            try
            {
                var updateSE = new Sword
                {
                    Id = id
                };

                var result = await _swordDal.DelElementToExistingSword(updateSE);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
