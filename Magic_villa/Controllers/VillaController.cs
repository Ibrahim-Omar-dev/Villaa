using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Magic_villa.Data;
using Magic_villa.Model;
using Magic_villa.Model.Dto.VillaDto;
using Magic_villa.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Magic_villa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VillaController> _logger;
        private readonly ApiResponse _apiResponse;

        public VillaController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<VillaController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _apiResponse = new ApiResponse(); 
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApiResponse>> GetVillas()
        {
            try
            {
                _logger.LogInformation("Getting all villas");
                var villas = await _unitOfWork.Villa.GetAll();
                _apiResponse.Result = _mapper.Map<List<VillaDto>>(villas);
                _apiResponse.StatusCodes = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all villas");
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCodes = HttpStatusCode.InternalServerError;
                _apiResponse.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiResponse);
            }
        }

        [HttpGet("{id:int}", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> GetVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Get Villa Error with Id {Id}", id);
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessage = "Invalid id";
                    return BadRequest(_apiResponse);
                }

                var villa = await _unitOfWork.Villa.Get(v => v.Id == id);
                if (villa == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessage = $"Villa with id {id} not found";
                    return NotFound(_apiResponse);
                }

                _apiResponse.Result = _mapper.Map<VillaDto>(villa);
                _apiResponse.StatusCodes = HttpStatusCode.OK;
                _apiResponse.IsSuccess = true;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting villa with Id {Id}", id);
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCodes = HttpStatusCode.InternalServerError;
                _apiResponse.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiResponse);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateVilla([FromBody] VillaCreateDto villaCreateDto)
        {
            try
            {
                if (villaCreateDto == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessage = "Payload is null";
                    return BadRequest(_apiResponse);
                }

                if (!ModelState.IsValid)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                    var errors = ModelState.Values
                                           .SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage)
                                           .ToList();
                    _apiResponse.ErrorMessage = string.Join(" | ", errors);
                    return BadRequest(_apiResponse);
                }

                var existing = await _unitOfWork.Villa.Get(v => v.Name.ToLower() == villaCreateDto.Name.ToLower());
                if (existing != null)
                {
                    ModelState.AddModelError("CustomError", "Villa already exists!");
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                    var dupErrors = ModelState.Values
                                              .SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                    _apiResponse.ErrorMessage = string.Join(" | ", dupErrors);
                    return BadRequest(_apiResponse);
                }

                var villa = _mapper.Map<Villa>(villaCreateDto);
                await _unitOfWork.Villa.Create(villa);
                await _unitOfWork.Save();

                _apiResponse.Result = _mapper.Map<VillaDto>(villa);
                _apiResponse.StatusCodes = HttpStatusCode.Created;
                _apiResponse.IsSuccess = true;
                return CreatedAtRoute("GetVilla", new { id = villa.Id }, _apiResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a villa");
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCodes = HttpStatusCode.InternalServerError;
                _apiResponse.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiResponse);
            }
        }

        // Use PUT for full update (or PATCH with proper patch document handling)
        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        {
            try
            {
                if (villaUpdateDto == null || id == 0)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessage = "Invalid payload or id";
                    return BadRequest(_apiResponse);
                }

                var villaFromDb = await _unitOfWork.Villa.Get(v => v.Id == id);
                if (villaFromDb == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessage = $"Villa with id {id} not found";
                    return NotFound(_apiResponse);
                }

                _mapper.Map(villaUpdateDto, villaFromDb);

                await _unitOfWork.Villa.Update(villaFromDb);
                await _unitOfWork.Save();

                _apiResponse.IsSuccess = true;
                _apiResponse.StatusCodes = HttpStatusCode.OK;
                _apiResponse.Result = _mapper.Map<VillaDto>(villaFromDb);

                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCodes = HttpStatusCode.InternalServerError;
                _apiResponse.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiResponse);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.BadRequest;
                    _apiResponse.ErrorMessage = "Invalid id";
                    return BadRequest(_apiResponse);
                }

                var villaFromDb = await _unitOfWork.Villa.Get(v => v.Id == id);
                if (villaFromDb == null)
                {
                    _apiResponse.IsSuccess = false;
                    _apiResponse.StatusCodes = HttpStatusCode.NotFound;
                    _apiResponse.ErrorMessage = $"Villa with id {id} not found";
                    return NotFound(_apiResponse);
                }

                _unitOfWork.Villa.Remove(villaFromDb);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.StatusCodes = HttpStatusCode.InternalServerError;
                _apiResponse.ErrorMessage = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, _apiResponse);
            }
        }
    }
}
