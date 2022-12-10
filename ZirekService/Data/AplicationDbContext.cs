using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZirekService.Models;

namespace ZirekService.Data {
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string> {
        public DbSet<BaseAudit> BaseAudits { get; set; }
        public DbSet<StatisticClassificator> StatisticClassificator { get; set; }
        public DbSet<StatisticEntity> StatisticEntity { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
        }

    }
}
