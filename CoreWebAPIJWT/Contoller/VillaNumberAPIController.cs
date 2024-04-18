using AutoMapper;
using Azure;
using CoreWebAPIJWT.Models;
using CoreWebAPIJWT.Models.DTO;
using CoreWebAPIJWT.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CoreWebAPIJWT.Contoller
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IMapper _mapper;
        public VillaNumberAPIController(IVillaNumberRepository dbVillaNumber, IMapper mapper)
        {
            _dbVillaNumber = dbVillaNumber;
            _mapper = mapper;
            this._response = new();
        }

        [HttpGet]
       // [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbers()
        {
            try
            {
                IEnumerable<VillaNumber> VillaNumberList = await _dbVillaNumber.GetAllAsync();
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(VillaNumberList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.IsSuccess = false;

            }
            return _response;
        }
        [HttpGet("{id:int}",Name="GetVillaNumber")]
        public async Task<ActionResult<APIResponse>> GetVillaNumber(int id)
        {
            try
            {
                if (id == 0) 
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villa=await _dbVillaNumber.GetAsync(w=>w.VillaNo==id);
                if(villa == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result=_mapper.Map<VillaNumberDTO>(villa);
                _response.StatusCode = HttpStatusCode.OK;
                 return Ok(villa);

            }
            catch (Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.IsSuccess = false;

            }
            return _response;

        }

        [HttpPost]
       
        public async Task<ActionResult<APIResponse>> CreateVillaNumber([FromBody] VillaNumberCreateDTO NumberCreateDTO)
        {
            try
            {
                if (await _dbVillaNumber.GetAsync(u => u.SpecialDetails.ToLower() == NumberCreateDTO.SpecialDetails.ToLower()) != null)
                {
                    ModelState.AddModelError("Custome Error", "Villa is Already Exists");
                    return BadRequest(ModelState);
                }
                if (NumberCreateDTO == null)
                {
                    return BadRequest(NumberCreateDTO);
                }
                var v=_mapper.Map<VillaNumber>(NumberCreateDTO);
                await _dbVillaNumber.CreateAsync(v);
                _response.Result=_mapper.Map<VillaNumberCreateDTO>(NumberCreateDTO);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = v.VillaNo }, _response);
            }
            catch (Exception ex)
            {
                _response.ErrorMessege = new List<string> { ex.Message };
                _response.IsSuccess = false;

            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteVillaNumber")]
        public async Task<ActionResult<VillaNumberDTO>> DeleteVillaNumber(int id)
        {
            try
            {
                if (id==null)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var villanumbr= await _dbVillaNumber.GetAsync(a=>a.VillaNo==id);
                if (villanumbr==null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                await _dbVillaNumber.RemoveAsync(villanumbr);
                _response.StatusCode=HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessege = new List<string> { ex.ToString() };
            }
            return Ok(_response);
        }

        [HttpPut("{id:int}", Name = "UpdateVillaNumber")]
        public  async Task<ActionResult<APIResponse>> UpdateVillaNumber(int id, [FromBody] VillaNumberUpdateDTO NumberUpdateDTO )
        {
            try
            {
                if (NumberUpdateDTO == null || id != NumberUpdateDTO.VillaNo)
                {
                    return BadRequest();
                }

                VillaNumber model = _mapper.Map<VillaNumber>(NumberUpdateDTO);
                await _dbVillaNumber.UpdateNumberAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessege = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialVillaNumber")]
        public async Task<ActionResult<APIResponse>> UpdatePartialVillaNumber(int id, JsonPatchDocument<VillaNumberUpdateDTO> NumberUpdateDTO)
        {
            try
            {
                if (NumberUpdateDTO == null || id ==null)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var v = await _dbVillaNumber.GetAsync(x=>x.VillaNo==id,tracked :false);

                VillaNumberUpdateDTO dto = _mapper.Map<VillaNumberUpdateDTO>(v);
                if (dto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                NumberUpdateDTO.ApplyTo(dto, ModelState);

                VillaNumber model = _mapper.Map<VillaNumber>(dto);

                await _dbVillaNumber.UpdateNumberAsync(model);

                if(ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessege = new List<string> { ex.ToString() };
            }
            return _response;
        }



    }
}
