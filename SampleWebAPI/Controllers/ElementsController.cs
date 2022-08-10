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
    public class ElementsController : ControllerBase
    {
        private readonly IElement _elementDal;
        private readonly IMapper _mapper;

        public ElementsController(IElement elementDal, IMapper mapper)
        {
            _elementDal = elementDal;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IEnumerable<ElementReadDTO>> Get()
        {
            List<ElementReadDTO> elementDtos = new List<ElementReadDTO>();
            var results = await _elementDal.GetAll();
            foreach (var result in results)
            {
                elementDtos.Add(new ElementReadDTO
                {
                    ElementId = result.ElementId,
                    Name = result.Name                   
                });
            }
            return elementDtos;
        }
        [HttpPost]
        public async Task<ActionResult> Post(ElementCreateDTO elementCreateDTO)
        {
            try
            {
                var newElement = _mapper.Map<Element>(elementCreateDTO);
                var result = await _elementDal.Insert(newElement);
                var swordReadDTO = _mapper.Map<ElementReadDTO>(result);

                return CreatedAtAction("Get", new
                {
                    id = result.ElementId
                }, swordReadDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Put(ElementReadDTO elementDto)
        {
            try
            {
                var updateElement = new Element
                {
                    ElementId = elementDto.ElementId,
                    Name = elementDto.Name      
                };
                var result = await _elementDal.Update(updateElement);
                return Ok(elementDto);
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
                await _elementDal.Delete(id);
                return Ok($"Data sword dengan id {id} berhasil didelete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ByName")]
        public async Task<IEnumerable<ElementReadDTO>> GetByName(string name)
        {
            List<ElementReadDTO> elementDtos = new List<ElementReadDTO>();
            var results = await _elementDal.GetByName(name);
            foreach (var result in results)
            {
                elementDtos.Add(new ElementReadDTO
                {
                    ElementId = result.ElementId,
                    Name = result.Name
                });
            }
            return elementDtos;
        }
    }
}
