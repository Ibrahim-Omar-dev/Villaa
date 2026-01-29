using AutoMapper;
using Magic_Villa_Web.Model;
using Magic_Villa_Web.Models;
using Magic_Villa_Web.Models.Dto.VillaDto;
using Magic_Villa_Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using Utility;

namespace Magic_Villa_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaServices _villaService;
        private readonly IMapper _mapper;
        public HomeController(IVillaServices villaService, IMapper mapper)
        {
            _villaService = villaService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDto> list = new();

            var response = await _villaService.GetAllAsync<ApiResponse>();
            if (response != null && response.IsSuccess)
            {
                list = JsonSerializer.Deserialize<List<VillaDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

    }

}
