using etickets.Data;
using etickets.Data.Services;
using etickets.Data.Static;
using etickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace etickets.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class CinemasController : Controller
    {
        protected readonly ICinemasService _service;
        public CinemasController(ICinemasService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")]Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }
        //Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id)
        {
            var cinemaDetail = await _service.GetByIdAsync(Id);
            if (cinemaDetail == null) return View("NotFound");
            return View(cinemaDetail);
        }
        //Edit and Update
        public async Task<IActionResult> Edit(int Id)
        {
            var cinemaDetail = await _service.GetByIdAsync(Id);
            if (cinemaDetail == null) return View("NotFound");
            return View(cinemaDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            if(Id == cinema.Id)
            {
                await _service.UpdateAsync(Id, cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
            
        }

        //Delete
        public async Task<IActionResult> Delete(int Id)
        {
            var cinemaDetail = await _service.GetByIdAsync(Id);
            if (cinemaDetail == null) return View("NotFound");
            return View(cinemaDetail);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var cinemaDetail = await _service.GetByIdAsync(Id);
            if (cinemaDetail == null) return View("NotFound");

            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));

        }
    }
}
