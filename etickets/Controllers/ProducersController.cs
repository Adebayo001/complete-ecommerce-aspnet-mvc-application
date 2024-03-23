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
    public class ProducersController : Controller
    {
        private readonly IProducersService _service;
        public ProducersController(IProducersService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }
        //GET Producer/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int Id)
        {
            var producerDetail = await _service.GetByIdAsync(Id);
            if (producerDetail == null) return View("NotFound");
            return View(producerDetail);
        }
        // Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }

            await _service.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        //Edit
        public async Task<IActionResult> Edit(int Id)
        {
            var producerDetails = await _service.GetByIdAsync(Id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, [Bind("Id,ProfilePictureURL,FullName,Bio")] Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            if(Id == producer.Id)
            {
                await _service.UpdateAsync(Id, producer);
                return RedirectToAction(nameof(Index));
            }
            return View(producer);
        }
        
        //Delete
        public async Task<IActionResult> Delete(int Id)
        {
            var producerDetails = await _service.GetByIdAsync(Id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var producerDetails = await _service.GetByIdAsync(Id);
            if (producerDetails == null) return View("NotFound");

            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
