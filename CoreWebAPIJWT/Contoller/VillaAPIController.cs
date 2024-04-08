using CoreWebAPIJWT.Data;
//using CoreWebAPIJWT.Logging;
using CoreWebAPIJWT.Models;
using CoreWebAPIJWT.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CoreWebAPIJWT.Contoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController2 : ControllerBase
    {   //custome logger
        //logging constructor injection implementations
        //private readonly ILogging _logger;
        //public VillaAPIController(ILogging logger) 
        //{
        // _logger = logger;
        //}
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            //Log("Getting All Villas.", "");
            return Ok(VillaStore.VillaList);
        }

        //to retrive a particuler villa
        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(404)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            //checking validation
            if (id == 0)
            {
                //Log("Get Villa Error with Id"+id, "error");
                return BadRequest();
            }
            var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
            if (v == null)
            {
                return NotFound();

            }
            return Ok(v);
        }


        //Create a new villa data 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            //if(ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //Custome Validation
            if (VillaStore.VillaList.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custome Error", "Villa is Already Exists");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            villaDTO.Id = VillaStore.VillaList.OrderByDescending(w => w.Id).FirstOrDefault().Id + 1;
            VillaStore.VillaList.Add(villaDTO);
            return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        }
        //delete the data by id
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public ActionResult<VillaDTO> DeleteVillaById(int id)
        {
            //checking validation
            if (id == 0)
            {
                return BadRequest();
            }
            var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
            if (v == null)
            {
                return NotFound();

            }
            VillaStore.VillaList.Remove(v);
            return NoContent();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]//showimg the status of requests
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
            v.Name = villaDTO.Name;
            v.Sqft = villaDTO.Sqft;
            v.Occupancy = villaDTO.Occupancy;
            return NoContent();


        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            //validate the id
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
            if (v == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(v, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();

        }
    }
}