using MagicVilla_API.Data;
using MagicVilla_API.Models;
using MagicVilla_API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDBContext _db;

        public VillaController(ILogger<VillaController> logger, ApplicationDBContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            _logger.LogInformation("Obtener todas las villas");
            return Ok(_db.Villas.ToList());
        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {

            if (id == 0)
            {
                _logger.LogError("Error al obtener la villa con el id " + id);
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.id == id);

            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_db.Villas.FirstOrDefault(v => v.name.ToLower() == villaDTO.name.ToLower()) != null)
            {
                ModelState.AddModelError("NameExists", "La Villa con ese nombre ya existe");
                return BadRequest(ModelState);
            }

            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            Villa model = new()
            {
                name = villaDTO.name,
                detail = villaDTO.detail,
                urlImage = villaDTO.urlImage,
                occupants = villaDTO.occupants,
                fee = villaDTO.fee,
                squareMeter = villaDTO.squareMeter,
                amenity = villaDTO.amenity
            };

            _db.Villas.Add(model);
            _db.SaveChanges();

            return CreatedAtRoute("GetVilla", new { id = villaDTO.id, villaDTO });

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.FirstOrDefault(v => v.id == id);

            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if(villaDTO == null || id != villaDTO.id)
            {
                return BadRequest();
            }

            Villa model = new()
            {
                id = villaDTO.id,
                name = villaDTO.name,
                detail = villaDTO.detail,
                urlImage = villaDTO.urlImage,
                occupants = villaDTO.occupants,
                fee = villaDTO.fee,
                squareMeter = villaDTO.squareMeter,
                amenity = villaDTO.amenity
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var villa = _db.Villas.AsNoTracking().FirstOrDefault(v=>v.id == id);

            VillaDTO villaDTO = new()
            {
                id = villa.id,
                name = villa.name,
                detail = villa.detail,
                urlImage = villa.urlImage,
                occupants = villa.occupants,
                fee = villa.fee,
                squareMeter = villa.squareMeter,
                amenity = villa.amenity
            };

            if (villa == null) return BadRequest();

            patchDTO.ApplyTo(villaDTO, ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Villa model = new()
            {
                id = villaDTO.id,
                name = villaDTO.name,
                detail = villaDTO.detail,
                urlImage = villaDTO.urlImage,
                occupants = villaDTO.occupants,
                fee = villaDTO.fee,
                squareMeter = villaDTO.squareMeter,
                amenity = villaDTO.amenity
            };

            _db.Villas.Update(model);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
