using BloodBankManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloodBankManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Donor> Donors { get; set; }

        public DbSet<BloodUnit> BloodUnits { get; set; }

        public DbSet<BloodRequest> BloodRequests { get; set; }

        public DbSet<BloodIssue> BloodIssues { get; set; }
    }
}