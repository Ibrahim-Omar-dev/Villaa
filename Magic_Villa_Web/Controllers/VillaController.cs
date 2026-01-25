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
        public VillaController(IVillaServices villaServices, IMapper mapper)
        {
            VillaServices = villaServices;
            Mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            var response = await VillaServices.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                var resultString = response.Result.ToString();
                var villas = JsonSerializer.Deserialize<List<VillaDto>>(
                    resultString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );

                //foreach (var villa in villas)
                //{
                //    Console.WriteLine($"Villa: {villa.Name}, ID: {villa.Id}");
                //}

                return View(villas);
            }
            return View(new List<VillaDto>());
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

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await VillaServices.GetByIdAsync<ApiResponse>(id);
            if (response != null && response.IsSuccess)
            {
                var resultString = response.Result.ToString();
                var villaDto = JsonSerializer.Deserialize<VillaDto>(
                    resultString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                return View(Mapper.Map<VillaUpdateDto>(villaDto));
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(VillaUpdateDto model)
        {
            if (ModelState.IsValid)
            {
                var response = await VillaServices.UpdateAsync<ApiResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        
        public async Task<IActionResult> Delete(int id)
        {
            var response = await VillaServices.GetByIdAsync<ApiResponse>(id);
            if (response != null && response.IsSuccess)
            {
                var resultString = response.Result.ToString();
                var villaDto = JsonSerializer.Deserialize<VillaDto>(
                    resultString,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                );
                return View(villaDto);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VillaDto model)
        {
            var response = await VillaServices.DeleteAsync<ApiResponse>(model.Id);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Villa deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
