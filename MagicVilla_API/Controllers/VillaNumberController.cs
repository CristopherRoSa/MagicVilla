using AutoMapper;
using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using MagicVilla_API.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly ILogger<VillaNumberController> _logger;
        private readonly IVillaRepository _villaRepository;
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IMapper _mapper;
        protected APIResponse _response;

        public VillaNumberController(ILogger<VillaNumberController> logger, 
            IVillaRepository villaRepository, IVillaNumberRepository villaNumberRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _villaRepository = villaRepository;
            _villaNumberRepository = villaNumberRepository;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillasNumbers()
        {

            try
            {
                _logger.LogInformation("Obtener todos los números de villas");

                IEnumerable<VillaNumber> villaNumberList = await _villaNumberRepository.GetAll();

                _response.result = _mapper.Map<IEnumerable<VillaDTO>>(villaNumberList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccessful = false;
                _response.errorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("id:int", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {

            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al obtener la villa con el id " + id);
                    _response.statusCode = HttpStatusCode.BadRequest;
                    _response.isSuccessful = false;
                    return BadRequest(_response);
                }

                var villaNumber = await _villaNumberRepository.Get(v => v.villaNum == id);

                if (villaNumber == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    _response.isSuccessful = false;
                    return NotFound(_response);
                }

                _response.result = _mapper.Map<VillaNumberDTO>(villaNumber);

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccessful = false;
                _response.errorMessage = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {

            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (await _villaNumberRepository.Get(v => v.villaNum == createDTO.villaNum) != null)
                {
                    ModelState.AddModelError("NameExists", "El número de villa ya existe");
                    return BadRequest(ModelState);
                }

                if(await _villaRepository.Get(v => v.id == createDTO.villaNum) == null)
                {
                    ModelState.AddModelError("ForeignKey", "El id de la villa no existe");
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(createDTO);
                model.creationDate = DateTime.Now;
                model.updateDate = DateTime.Now;

                await _villaNumberRepository.Create(model);
                _response.result = model;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVillaNumber", new { id = model.villaNum }, _response);
            }
            catch (Exception ex)
            {
                _response.isSuccessful = false;
                _response.errorMessage = new List<string>() { ex.ToString() };
            }
            
            return _response;
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.isSuccessful = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var villaNumber = await _villaNumberRepository.Get(v => v.villaNum == id);

                if (villaNumber == null)
                {
                    _response.isSuccessful = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _villaNumberRepository.Remove(villaNumber);

                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.isSuccessful = false;
                _response.errorMessage = new List<string>() { ex.ToString() };
            }            

            return BadRequest(_response);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO updateDTO)
        {
            if(updateDTO == null || id != updateDTO.villaNum)
            {
                _response.isSuccessful = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepository.Get(v => v.id == updateDTO.villaNum) == null)
            {
                ModelState.AddModelError("ForeignKey", "El id de la villa no existe");
                return BadRequest(ModelState);
            }

            VillaNumber model = _mapper.Map<VillaNumber>(updateDTO);            

            await _villaNumberRepository.Update(model);

            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }        

    }
}
