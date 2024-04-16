using AutoMapper;
using CoreWebAPIJWT.Data;
//using CoreWebAPIJWT.Logging;
using CoreWebAPIJWT.Models;
using CoreWebAPIJWT.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using CoreWebAPIJWT.Repository.IRepository;

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
        //private readonly ApplicationDBContext _dbContext;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        //public VillaAPIController(ApplicationDBContext dbContext,IMapper mapper)
        public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
        {
            _dbVilla= dbVilla;
            _mapper=mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        //async await default syntax

        public async Task<ActionResult<IEnumerable<VillaDTO>>>GetVillas()
        {
            //Log("Getting All Villas.", "");
            //return Ok(await _dbContext.Villas.ToListAsync());
            //IEnumerable<Villa> villaList = await _dbContext.Villas.ToListAsync();
            IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();
            return Ok(_mapper.Map<List<VillaDTO>>(villaList));
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
            //var v =await _dbContext.Villas.FirstOrDefaultAsync(w => w.Id == id);
            var v = await _dbVilla.GetAsync(w => w.Id == id);
            if (v == null)
            {
                return NotFound();

            }
            return Ok(_mapper.Map<VillaDTO>(v));
        }


        //Create a new villa data 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDTO>> CreateVilla([FromBody] VillaCreateDTO CreateDTO)
        {
           
            //Custome Validation
            if (await _dbVilla.GetAsync(u => u.Name.ToLower() == CreateDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Custome Error", "Villa is Already Exists");
                return BadRequest(ModelState);
            }
            if (CreateDTO == null)
            {
                return BadRequest(CreateDTO);
            }
            
            //its automatically maps all properties of DTO
            Villa model = _mapper.Map<Villa>(CreateDTO);

            //Villa model = new()
            //{
            //    Amenity = villaCreateDTO.Amenity,
            //    Details = villaCreateDTO.Details,
            //    Occupancy = villaCreateDTO.Occupancy,
            //    ImageUrl = villaCreateDTO.ImageUrl,
            //    Name = villaCreateDTO.Name,
            //    Rate = villaCreateDTO.Rate,
            //    Sqft = villaCreateDTO.Sqft,

            //};
            await _dbVilla.CreateAsync(model);

           await _dbVilla.SaveAsync();

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
        public async Task<ActionResult<VillaDTO>> DeleteVilla(int id)
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
            var villa =await  _dbVilla.GetAsync(w => w.Id == id);
            if (villa == null)
            {
                return NoContent();
            }
            await _dbVilla.RemoveAsync(villa);
            return NoContent();
        }


        [ProducesResponseType(StatusCodes.Status204NoContent)]//showimg the status of requests
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id:int}", Name = "UpdateVilla")]

        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDTO UpdateDTO)
        {
            if (UpdateDTO == null || id != UpdateDTO.Id)
            {
                return BadRequest();
            }
            //var v = VillaStore.VillaList.FirstOrDefault(w => w.Id == id);
            //v.Name = villaDTO.Name;
            //v.Sqft = villaDTO.Sqft;
            //v.Occupancy = villaDTO.Occupancy;

            //its automatically maps all properties of DTO
            Villa model = _mapper.Map<Villa>(UpdateDTO);

            //using entity framework
            //Villa model = new()
            //{
            //    Amenity = villaDTO.Amenity,
            //    Details = villaDTO.Details,
            //    Occupancy = villaDTO.Occupancy,
            //    Id = villaDTO.Id,
            //    ImageUrl = villaDTO.ImageUrl,
            //    Name = villaDTO.Name,
            //    Rate = villaDTO.Rate,
            //    Sqft = villaDTO.Sqft,

            //};
             await _dbVilla.UpdateAsync(model);
            await _dbVilla.SaveAsync();

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

        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDTO> patchDTO)
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
            var villa =await _dbVilla.GetAsync(x => x.Id == id,tracked:false);

            //villa.Name = "new name";
            //_dbContext.SaveChanges();

            //its automatically maps all properties of DTO
            VillaUpdateDTO DTO = _mapper.Map<VillaUpdateDTO>(villa);           


            //VillaDTO dto = new()
            //{
            //    Amenity = villa.Amenity,
            //    Details = villa.Details,
            //    Occupancy = villa.Occupancy,
            //    Id = villa.Id,
            //    ImageUrl = villa.ImageUrl,
            //    Name = villa.Name,
            //    Rate = villa.Rate,
            //    Sqft = villa.Sqft,

            //};
            if (villa  == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(DTO, ModelState);           

            Villa model =_mapper.Map<Villa>(DTO);

            //Villa model = new()
            //{
            //    Amenity = dto.Amenity,
            //    Details = dto.Details,
            //    Occupancy = dto.Occupancy,
            //    Id = dto.Id,
            //    ImageUrl = dto.ImageUrl,
            //    Name = dto.Name,
            //    Rate = dto.Rate,
            //    Sqft = dto.Sqft,

            //};
           await _dbVilla.UpdateAsync(model);
           await _dbVilla.SaveAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();

        }
    }
}