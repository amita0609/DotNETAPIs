using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("/api/VillaNumberAPI")]
    [ApiController]
    public class VillaNumberAPIController:ControllerBase
    {
        // private readonly ILogging _logger;

        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;

        protected APIResponse _response;
        
        public VillaNumberAPIController(IVillaNumberRepository db,IMapper mapper, IVillaRepository dbVilla)
        {
            _dbVillaNumber = db;
            _mapper = mapper;
            this._response = new();
            _dbVilla = dbVilla;
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> villaList = await _dbVillaNumber.GetAll();
                _response.Result = _mapper.Map<List<VillaDTO>>(villaList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return _response;
        }





        /*  [ProducesResponseType(200)]
       [ProducesResponseType(404)]
       [ProducesResponseType(400)] */
        [HttpGet("{id:int}",Name ="GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa = await _dbVillaNumber.Get(u => u.VillaNo == id);
                if (villa == null)
                {
                    return NotFound();
                }
                _response.Result = _mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return _response;

           
           
        }



        //post
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
        {
            try
            {
                //custom modelstate validation
                if (await _dbVillaNumber.Get(u => u.VillaNo == createDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("Custom Error", "Villa already exixts");
                    return BadRequest(ModelState);
                }
                if (await _dbVilla.Get(u => u.Id == createDTO.VillaID) == null)
                {
                    ModelState.AddModelError("Custom Error", "VillaId is invalid");
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
                VillaNumber model = _mapper.Map<VillaNumber>(createDTO);



                await _dbVillaNumber.Create(model);

                _response.Result = _mapper.Map<VillaDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetVilla", new { id = model.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return _response;

         
        }




        //Update
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO villaDTO)
        {
            try
            {
                if (villaDTO == null || id != villaDTO.VillaNo)
                {
                    return BadRequest();
                }
                if (await _dbVilla.Get(u => u.Id == villaDTO.VillaID) == null)
                {
                    ModelState.AddModelError("Custom Error", "VillaId is invalid");
                    return BadRequest(ModelState);

                }
              

                VillaNumber model = _mapper.Map<VillaNumber>(villaDTO);



                await _dbVillaNumber.UpdateAsync(model);


                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<String>() { ex.ToString() };
            }
            return _response;

        }

        //patch request
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPatch("{id:int}", Name = "UpdatePartialVillaNumber")]
        public async Task <IActionResult> UpdatePartialVillaNumber(int id, JsonPatchDocument< VillaUpdateDTO> patchDTO)
        {
           
                if (patchDTO == null || id == 0)
                {
                    return BadRequest();
                }
                var villa = await _dbVillaNumber.Get(u => u.VillaNo == id, tracked: false);

                VillaUpdateDTO villaDTO = _mapper.Map<VillaUpdateDTO>(villa);

                //VillaUpdateDTO villaDTO = new()
                //{
                //    Id = villa.Id,
                //    Name = villa.Name,
                //    Amenity = villa.Amenity,
                //    Details = villa.Details,
                //    ImageUrl = villa.ImageUrl,
                //    Occupancy = villa.Occupancy,
                //    Rate = villa.Rate,
                //    Sqft = villa.Sqft
                //};

                if (villa == null)
                {
                    return BadRequest();
                }
                patchDTO.ApplyTo(villaDTO, ModelState);

                VillaNumber model = _mapper.Map<VillaNumber>(villaDTO);


            

                await _dbVillaNumber.UpdateAsync(model);

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
        [ HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _dbVillaNumber.Get(u => u.VillaNo == id);
            if (villa == null)
            {
                return NotFound();
            }
            await _dbVillaNumber.remove(villa);

            
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess = true;
            return Ok(_response);
        }



    }
}
