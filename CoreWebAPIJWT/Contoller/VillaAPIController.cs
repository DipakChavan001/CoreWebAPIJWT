using CoreWebAPIJWT.Data;
//using CoreWebAPIJWT.Logging;
using CoreWebAPIJWT.Models;
using CoreWebAPIJWT.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPIJWT.Contoller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VillaAPIController : ControllerBase
    {   //custome logger
        //logging constructor injection implementations
        //private readonly ILogging _logger;
        //public VillaAPIController(ILogging logger) 
        //{
        // _logger = logger;
        //}



        //use dependency injection  extract services
        private readonly ApplicationDBContext _dbContext;
        public VillaAPIController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //async await default syntax

        public async Task<ActionResult<IEnumerable<VillaDTO>>>GetVillas()
        {
            //Log("Getting All Villas.", "");
            return Ok(await _dbContext.Villas.ToListAsync());
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
        public async Task<ActionResult<VillaDTO>> GetVilla(int id)
        {
            //checking validation
            if (id == 0)
            {
                //Log("Get Villa Error with Id"+id, "error");
                return BadRequest();
            }
            var v =await _dbContext.Villas.FirstOrDefaultAsync(w => w.Id == id);
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
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO villaDTO)
        {
           
            //Custome Validation
            if (await _dbContext.Villas.FirstOrDefaultAsync(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custome Error", "Villa is Already Exists");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            

            //enter the data mapping to tables by using a enetity framework
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Occupancy = villaDTO.Occupancy,              
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,

            };
            await _dbContext.Villas.AddAsync(model);

           await _dbContext.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);
        }


        //public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        //{
        //    //if(ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}
        //    //Custome Validation
        //    if (_dbContext.Villas.ToList().FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
        //    {
        //        ModelState.AddModelError("Custome Error", "Villa is Already Exists");
        //        return BadRequest(ModelState);
        //    }
        //    if (villaDTO == null)
        //    {
        //        return BadRequest(villaDTO);
        //    }
        //    if (villaDTO.Id > 0)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);

        //    }
        //    //villaDTO.Id = VillaStore.VillaList.OrderByDescending(w => w.Id).FirstOrDefault().Id + 1;            
        //    //VillaStore.VillaList.Add(villaDTO);
        //    //

        //    //enter the data mapping to tables by using a enetity framework
        //    Villa model = new()
        //    {
        //        Amenity = villaDTO.Amenity,
        //        Details = villaDTO.Details,
        //        Occupancy = villaDTO.Occupancy,
        //        Id = villaDTO.Id,
        //        ImageUrl = villaDTO.ImageUrl,
        //        Name = villaDTO.Name,
        //        Rate = villaDTO.Rate,
        //        Sqft = villaDTO.Sqft,

        //    };
        //    _dbContext.Villas.Add(model);
        //    _dbContext.SaveChanges();

        //    return CreatedAtRoute("GetVilla", new { id = villaDTO.Id }, villaDTO);
        //}
        //delete the data by id

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        public async Task<ActionResult<VillaDTO>> DeleteVillaById(int id)
        {
            //checking validation
            if (id == 0)
            {
                return BadRequest();
            }
            //hard coded value
            ////var v=Villastore.villaList.FirstorDefault(u=>u.Id==id);          

            //if (v == null)
            //{
            //    return NotFound();

            //}
            //VillaStore.VillaList.Remove(v);
            //var v = _dbContext.Villas.ToList().FirstOrDefault(w => w.Id == id);
            var v =await  _dbContext.Villas.FirstOrDefaultAsync(w => w.Id == id);
            if (v == null)
            {
                return NoContent();
            }
            _dbContext.Villas.Remove(v);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]//showimg the status of requests
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]

        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            //var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
            //v.Name = villaDTO.Name;
            //v.Sqft = villaDTO.Sqft;
            //v.Occupancy = villaDTO.Occupancy;

            //using entity framework
            Villa model = new()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Occupancy = villaDTO.Occupancy,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft,

            };
            _dbContext.Villas.Update(model);
           await _dbContext.SaveChangesAsync();

            return NoContent();


        }
        //public IActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        //{
        //    if (villaDTO == null || id != villaDTO.Id)
        //    {
        //        return BadRequest();
        //    }
        //    //var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
        //    //v.Name = villaDTO.Name;
        //    //v.Sqft = villaDTO.Sqft;
        //    //v.Occupancy = villaDTO.Occupancy;

        //    //using entity framework
        //    Villa model = new()
        //    {
        //        Amenity = villaDTO.Amenity,
        //        Details = villaDTO.Details,
        //        Occupancy = villaDTO.Occupancy,
        //        Id = villaDTO.Id,
        //        ImageUrl = villaDTO.ImageUrl,
        //        Name = villaDTO.Name,
        //        Rate = villaDTO.Rate,
        //        Sqft = villaDTO.Sqft,

        //    };
        //    _dbContext.Villas.Update(model);
        //    _dbContext.SaveChanges();

        //    return NoContent();


        //}

        [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            //validate the id
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            //var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
            //if (v == null)
            //{
            //    return BadRequest();
            //}
            //patchDTO.ApplyTo(v, ModelState);
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //using EF
            //creteical with EF with no tracking of id
            var villa =await _dbContext.Villas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            //villa.Name = "new name";
            //_dbContext.SaveChanges();

            VillaDTO dto = new()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Occupancy = villa.Occupancy,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Rate = villa.Rate,
                Sqft = villa.Sqft,

            };
            if (villa == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(dto, ModelState);

            Villa model = new()
            {
                Amenity = dto.Amenity,
                Details = dto.Details,
                Occupancy = dto.Occupancy,
                Id = dto.Id,
                ImageUrl = dto.ImageUrl,
                Name = dto.Name,
                Rate = dto.Rate,
                Sqft = dto.Sqft,

            };
            _dbContext.Villas.Update(model);
           await _dbContext.SaveChangesAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();

        }
    }
}