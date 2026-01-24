using AutoMapper;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto.VillaDto;
using Magic_Villa_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
namespace Magic_Villa_Web.Controllers
{
    public class VillaController : Controller
    {
        public IVillaServices VillaServices { get; }
        public IMapper Mapper { get; }
        public VillaController(IVillaServices villaServices,IMapper mapper)
        {
            VillaServices = villaServices;
            Mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            List<VillaDto> villaList = new();
            var apiResponse = await VillaServices.GetAllAsync<ApiResponse>();

            if (apiResponse != null && apiResponse.IsSuccess)
            {
                var resultString = apiResponse.Result.ToString();
                villaList = JsonSerializer.Deserialize<List<VillaDto>>(
                    resultString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

            }

            return View(villaList);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VillaCreateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await VillaServices.CreateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }
    }
}
