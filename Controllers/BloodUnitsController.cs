using BloodBankManagementSystem.Data;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManagementSystem.Controllers
{
    [Authorize]
    public class BloodUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bloodUnits = _context.BloodUnits.ToList();

            return View(bloodUnits);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BloodUnit bloodUnit)
        {
            if (ModelState.IsValid)
            {
                _context.BloodUnits.Add(bloodUnit);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bloodUnit);
        }

        public IActionResult Edit(int id)
        {
            var bloodUnit = _context.BloodUnits.Find(id);

            if (bloodUnit == null)
            {
                return NotFound();
            }

            return View(bloodUnit);
        }

        [HttpPost]
        public IActionResult Edit(BloodUnit bloodUnit)
        {
            if (ModelState.IsValid)
            {
                _context.BloodUnits.Update(bloodUnit);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bloodUnit);
        }

        public IActionResult Delete(int id)
        {
            var bloodUnit = _context.BloodUnits.Find(id);

            if (bloodUnit == null)
            {
                return NotFound();
            }

            _context.BloodUnits.Remove(bloodUnit);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}