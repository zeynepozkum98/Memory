using Memory.Business.Abstract;
using Memory.Entities.Concrete.Dtos;
using Memory.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Runtime.InteropServices;

namespace Memory.WebUI.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<CityDto> cities= await _cityService.GetAllCityAsync();
            return View(cities);
        }
           

          
 

        [HttpGet]
        public IActionResult AddCity() => View();
 
        [HttpPost]

        public async Task<IActionResult> AddCity(CityDto cityDto)
        {
            bool isTrue= await _cityService.AddCityAsync(cityDto);
           return isTrue ? RedirectToAction("Index"): View();
        }

        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            CityDto cityDto= await _cityService.GetCityByIdAsync(id);
            return View(cityDto);
        }

        [HttpPost]

        public async Task<IActionResult> Update(CityDto cityDto)
        {
           bool isTrue= await _cityService.UpdateCityAsync(cityDto);
           return isTrue ? RedirectToAction("Index"): View(cityDto);
          
        }

        [HttpGet]

        public async Task<IActionResult> Detail(int id)
        {
           CityDto cityDto= await _cityService.GetCityByIdAsync(id);
            return View(cityDto);
        }

        public async Task<IActionResult> Clear(int id)
        {
            bool response = await _cityService.DeleteCityAsync((await _cityService.GetCityByIdAsync(id)));
            return RedirectToAction("Index");
        }

      
    }
   
}
