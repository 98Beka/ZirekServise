using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZirekTeamAdmin.Models;

namespace ZirekTeamAdmin.Data {
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string> {
        public DbSet<BaseAudit> BaseAudits { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

    }
}
