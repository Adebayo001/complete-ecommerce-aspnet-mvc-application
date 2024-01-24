using etickets.Data;
using etickets.Data.Services;
using etickets.Models;
using Microsoft.AspNetCore.Mvc;

namespace etickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")]Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //GET Actors/Details/1
        public async Task<IActionResult> Details(int Id)
        {
            var actorDetails = await _service.GetByIdAsync(Id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        //Edit
        public async Task<IActionResult> Edit(int Id)
        {
            var actorDetails = await _service.GetByIdAsync(Id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("Id, FullName,ProfilePictureURL,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(Id, actor);
            return RedirectToAction(nameof(Index));
        }
        //Edit
        public async Task<IActionResult> Delete(int Id)
        {
            var actorDetails = await _service.GetByIdAsync(Id);
            if (actorDetails == null)
            {
                return View("NotFound");
            }
            return View(actorDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var actorDetails = await _service.GetByIdAsync(Id);
            if (actorDetails == null) return View("NotFound");
            
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
