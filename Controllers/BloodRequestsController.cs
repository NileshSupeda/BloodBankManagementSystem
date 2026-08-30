using BloodBankManagementSystem.Data;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManagementSystem.Controllers
{
    [Authorize]
    public class BloodRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var requests = _context.BloodRequests.ToList();

            return View(requests);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BloodRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.BloodRequests.Add(request);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(request);
        }

        public IActionResult Edit(int id)
        {
            var request = _context.BloodRequests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [HttpPost]
        public IActionResult Edit(BloodRequest request)
        {
            if (ModelState.IsValid)
            {
                _context.BloodRequests.Update(request);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(request);
        }

        public IActionResult Delete(int id)
        {
            var request = _context.BloodRequests.Find(id);

            if (request == null)
            {
                return NotFound();
            }

            _context.BloodRequests.Remove(request);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}