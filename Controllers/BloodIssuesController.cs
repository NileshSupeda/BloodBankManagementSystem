using BloodBankManagementSystem.Data;
using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManagementSystem.Controllers
{
    [Authorize]
    public class BloodIssuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BloodIssuesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bloodIssues = _context.BloodIssues.ToList();

            return View(bloodIssues);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BloodIssue bloodIssue)
        {
            if (!ModelState.IsValid)
            {
                return View(bloodIssue);
            }

            if (bloodIssue.UnitsIssued <= 0)
            {
                ModelState.AddModelError(
                    "UnitsIssued",
                    "Units issued must be greater than 0."
                );

                return View(bloodIssue);
            }

            var availableBloodUnits = _context.BloodUnits
                .Where(b =>
                    b.BloodGroup == bloodIssue.BloodGroup &&
                    b.Status == "Available")
                .OrderBy(b => b.ExpiryDate)
                .Take(bloodIssue.UnitsIssued)
                .ToList();

            if (availableBloodUnits.Count < bloodIssue.UnitsIssued)
            {
                ModelState.AddModelError(
                    "UnitsIssued",
                    $"Only {availableBloodUnits.Count} unit(s) of {bloodIssue.BloodGroup} are available."
                );

                return View(bloodIssue);
            }

            foreach (var bloodUnit in availableBloodUnits)
            {
                bloodUnit.Status = "Issued";
            }

            _context.BloodIssues.Add(bloodIssue);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var bloodIssue = _context.BloodIssues.Find(id);

            if (bloodIssue == null)
            {
                return NotFound();
            }

            return View(bloodIssue);
        }

        [HttpPost]
        public IActionResult Edit(BloodIssue bloodIssue)
        {
            if (!ModelState.IsValid)
            {
                return View(bloodIssue);
            }

            var existingIssue = _context.BloodIssues.Find(
                bloodIssue.BloodIssueId);

            if (existingIssue == null)
            {
                return NotFound();
            }

            existingIssue.PatientName = bloodIssue.PatientName;
            existingIssue.BloodGroup = bloodIssue.BloodGroup;
            existingIssue.HospitalName = bloodIssue.HospitalName;
            existingIssue.ContactNumber = bloodIssue.ContactNumber;
            existingIssue.IssueDate = bloodIssue.IssueDate;
            existingIssue.Status = bloodIssue.Status;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var bloodIssue = _context.BloodIssues.Find(id);

            if (bloodIssue == null)
            {
                return NotFound();
            }

            _context.BloodIssues.Remove(bloodIssue);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}