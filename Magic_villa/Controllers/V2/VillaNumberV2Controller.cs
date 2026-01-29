using AutoMapper;
using Magic_villa.Model;
using Magic_villa.Model.Dto.VillaNumberDto;
using Magic_villa.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Magic_villa.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VillaNumberV2Controller : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<VillaNumberV2Controller> _logger;

        public VillaNumberV2Controller(IUnitOfWork unitOfWork, IMapper mapper, ILogger<VillaNumberV2Controller> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }


        // GET: api/v2/VillaNumber/test - V2 specific endpoint
        [HttpGet("test")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetTest()
        {
            var response = new ApiResponse
            {
                IsSuccess = true,
                StatusCodes = HttpStatusCode.OK,
                Result = new { Message = "This is V2 API", Values = new string[] { "value1", "value2" } }
            };
            return Ok(response);
        }

        // GET: api/v2/VillaNumber/{id}
        [HttpGet("{id:int}", Name = "GetVillaNumberV2")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> GetById(int id)
        {
            var response = new ApiResponse();
            try
            {
                var villaNumber = await _unitOfWork.VillaNumber.Get(vn => vn.VillaNum == id, includeProperty: "Villa");
                if (villaNumber == null)
                {
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.NotFound;
                    response.ErrorMessage = $"Villa number {id} not found.";
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.StatusCodes = HttpStatusCode.OK;
                response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to get villa number {Id}", id);
                response.IsSuccess = false;
                response.StatusCodes = HttpStatusCode.InternalServerError;
                response.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // POST: api/v2/VillaNumber
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Create([FromBody] VillaNumberCreateDto createDto)
        {
            var response = new ApiResponse();
            try
            {
                if (createDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.BadRequest;
                    response.ErrorMessage = "Request body is null.";
                    return BadRequest(response);
                }

                if (!ModelState.IsValid)
                {
                    var modelErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.BadRequest;
                    response.ErrorMessage = string.Join(" | ", modelErrors);
                    return BadRequest(response);
                }

                // Duplicate check
                var exists = await _unitOfWork.VillaNumber.Get(vn => vn.VillaNum == createDto.VillaNum);
                if (exists != null)
                {
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.BadRequest;
                    response.ErrorMessage = $"Villa number {createDto.VillaNum} already exists.";
                    return BadRequest(response);
                }

                // Foreign key check: ensure the referenced Villa exists
                var villa = await _unitOfWork.Villa.Get(v => v.Id == createDto.VillaId);
                if (villa == null)
                {
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.BadRequest;
                    response.ErrorMessage = $"Villa with id {createDto.VillaId} does not exist.";
                    return BadRequest(response);
                }

                var entity = _mapper.Map<VillaNumber>(createDto);
                await _unitOfWork.VillaNumber.Create(entity);
                await _unitOfWork.Save();

                response.IsSuccess = true;
                response.StatusCodes = HttpStatusCode.Created;
                response.Result = _mapper.Map<VillaNumberDto>(entity);

                return CreatedAtRoute("GetVillaNumberV2", new { id = entity.VillaNum }, response);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to create villa number");
                response.IsSuccess = false;
                response.StatusCodes = HttpStatusCode.InternalServerError;
                response.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        // DELETE: api/v2/VillaNumber/{id}
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var villaNumber = await _unitOfWork.VillaNumber.Get(vn => vn.VillaNum == id);
                if (villaNumber == null)
                {
                    var notFoundResponse = new ApiResponse
                    {
                        IsSuccess = false,
                        StatusCodes = HttpStatusCode.NotFound,
                        ErrorMessage = $"Villa number {id} not found."
                    };
                    return NotFound(notFoundResponse);
                }

                _unitOfWork.VillaNumber.Remove(villaNumber);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to delete villa number {Id}", id);
                var errorResponse = new ApiResponse
                {
                    IsSuccess = false,
                    StatusCodes = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message
                };
                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }

        // PUT: api/v2/VillaNumber/{id}
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> Update(int id, [FromBody] VillaNumberUpdateDto updateDto)
        {
            var response = new ApiResponse();
            try
            {
                if (updateDto == null)
                {
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.BadRequest;
                    response.ErrorMessage = "Request body is null.";
                    return BadRequest(response);
                }

                if (!ModelState.IsValid)
                {
                    var modelErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.BadRequest;
                    response.ErrorMessage = string.Join(" | ", modelErrors);
                    return BadRequest(response);
                }

                if (id != updateDto.VillaNum)
                {
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.BadRequest;
                    response.ErrorMessage = "Villa number ID mismatch.";
                    return BadRequest(response);
                }

                var villaNumber = await _unitOfWork.VillaNumber.Get(vn => vn.VillaNum == id);
                if (villaNumber == null)
                {
                    response.IsSuccess = false;
                    response.StatusCodes = HttpStatusCode.NotFound;
                    response.ErrorMessage = $"Villa number {id} not found.";
                    return NotFound(response);
                }

                // If update DTO contains VillaId, ensure it refers to an existing Villa
                if (updateDto.VillaId != 0)
                {
                    var villa = await _unitOfWork.Villa.Get(v => v.Id == updateDto.VillaId);
                    if (villa == null)
                    {
                        response.IsSuccess = false;
                        response.StatusCodes = HttpStatusCode.BadRequest;
                        response.ErrorMessage = $"Villa with id {updateDto.VillaId} does not exist.";
                        return BadRequest(response);
                    }
                }

                _mapper.Map(updateDto, villaNumber);
                _unitOfWork.VillaNumber.Update(villaNumber);
                await _unitOfWork.Save();

                response.IsSuccess = true;
                response.StatusCodes = HttpStatusCode.OK;
                response.Result = _mapper.Map<VillaNumberDto>(villaNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to update villa number {Id}", id);
                response.IsSuccess = false;
                response.StatusCodes = HttpStatusCode.InternalServerError;
                response.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}