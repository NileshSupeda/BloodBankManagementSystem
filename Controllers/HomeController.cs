using BloodBankManagementSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloodBankManagementSystem.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalDonors =
                _context.Donors.Count();

            ViewBag.TotalBloodUnits =
                _context.BloodUnits.Count();

            ViewBag.AvailableBloodUnits =
                _context.BloodUnits.Count(b => b.Status == "Available");

            ViewBag.AvailableStockByGroup = _context.BloodUnits
                .Where(b => b.Status == "Available")
                .GroupBy(b => b.BloodGroup)
                .Select(group => new { BloodGroup = group.Key, Count = group.Count() })
                .ToDictionary(item => item.BloodGroup, item => item.Count);

            ViewBag.TotalBloodRequests =
                _context.BloodRequests.Count();

            ViewBag.PendingRequests =
                _context.BloodRequests.Count(r => r.Status == "Pending");

            ViewBag.TotalBloodIssues =
                _context.BloodIssues.Count();

            return View();
        }
    }
}