using AutoMapper;
using Magic_Villa_Web.Model.ViewModel;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto.VillaDto;
using Magic_Villa_Web.Models.Dto.VillaNumberDto;
using Magic_Villa_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace Magic_Villa_Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberServices _villaNumberServices;
        private readonly IMapper _mapper;
        private readonly IVillaServices _villaServices;

        public VillaNumberController(
            IVillaNumberServices villaNumberServices,
            IVillaServices villaServices,
            IMapper mapper)
        {
            _villaNumberServices = villaNumberServices;
            _villaServices = villaServices;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaNumberDto> list = new();

            var response = await _villaNumberServices.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess && response.Result != null)
            {
                list = JsonSerializer.Deserialize<List<VillaNumberDto>>(
                    ((JsonElement)response.Result).GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var villaNumberCreateVM = new VillaNumberCreateVM();

            var response = await _villaServices.GetAllAsync<ApiResponse>();

            if (response != null && response.IsSuccess && response.Result != null)
            {
                var villas = JsonSerializer.Deserialize<List<VillaDto>>(
                    ((JsonElement)response.Result).GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                villaNumberCreateVM.VillaList = villas.Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }).ToList();
            }

            return View(villaNumberCreateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VillaNumberCreateVM model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberServices
                    .CreateAsync<ApiResponse>(model.villaNumber);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Villa Number created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    if (response?.ErrorMessage != null )
                    {
                        ModelState.AddModelError("", response.ErrorMessage);
                    }
                }
            }


            var villaResponse = await _villaServices.GetAllAsync<ApiResponse>();

            if (villaResponse != null && villaResponse.IsSuccess && villaResponse.Result != null)
            {
                var villas = JsonSerializer.Deserialize<List<VillaDto>>(
                    ((JsonElement)villaResponse.Result).GetRawText(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                model.VillaList = villas.Select(v => new SelectListItem
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }).ToList();
            }

            return View(model);
        }
    }
}