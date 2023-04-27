using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("/api/VillaAPI")]
    [ApiController]
    public class VillaAPIController:ControllerBase
    {
        // private readonly ILogging _logger;

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        
        public VillaAPIController(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
        {
            IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();
            return Ok(_mapper.Map<VillaDTO>(villaList));
        }





        /*  [ProducesResponseType(200)]
       [ProducesResponseType(404)]
       [ProducesResponseType(400)] */
        [HttpGet("{id:int}",Name ="GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            if (id == 0)
            {
              
                return BadRequest();
            }
            var villa =await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDTO>(villa));
        }



        //post
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<VillaDTO>> create([FromBody] VillaCreateDTO createDTO)
        {
            //custom modelstate validation
            if(await _db.Villas.FirstOrDefaultAsync(u=>u.Name.ToLower()==villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custom Error", "Villa already exixts");
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
          Villa model=  _mapper.Map<Villa>(createDTO);
 
         /*    Villa model = new()
            {
               
                Name= createDTO.Name,
                Amenity= createDTO.Amenity,
                Details= createDTO.Details,
                ImageUrl = createDTO.ImageUrl,
                Occupancy = createDTO.Occupancy,
                Rate= createDTO.Rate,
                Sqft= createDTO.Sqft
            };  */
            
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla",new {id=model.Id }, model);
        }




        //Update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            if(villaDTO==null || id!= villaDTO.Id)
            {
                return BadRequest();
            }
            /*  var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
              villa.Name = villaDTO.Name;
              villa.sqft = villaDTO.sqft;
              villa.Occupancy = villaDTO.Occupancy; */

            Villa model = _mapper.Map<Villa>(villaDTO);


       /*     Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };  */

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return NoContent();

        }

        //patch request
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        public async Task <IActionResult> UpdatePartialVilla(int id, JsonPatchDocument< VillaUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id ==0)
            {
                return BadRequest();
            }
            var villa =await _db.Villas.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            VillaUpdateDTO villaDTO = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Amenity = villa.Amenity,
                Details = villa.Details,
                ImageUrl = villa.ImageUrl,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };

            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(villaDTO,ModelState);
            Villa model = new()
            {
                Id = villaDTO.Id,
                Name = villaDTO.Name,
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                ImageUrl = villaDTO.ImageUrl,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            _db.Villas.Update(model);
             await  _db.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();

        }

        //Delete
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            _db.Villas.Remove(villa);
           await _db.SaveChangesAsync();
            return NoContent();
        }



    }
}
