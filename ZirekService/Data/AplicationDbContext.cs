using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ZirekService.Models;
using ZirekService.Models.Entities;

namespace ZirekService.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string> {
        public DbSet<BaseAuditEntity> BaseAudits { get; set; }
        public DbSet<StatisticClassificator> StatisticClassificators { get; set; }
        public DbSet<StatisticEntity> Statistics { get; set; }
        public DbSet<RuWordEnity> RuWords { get; set; }
        public DbSet<EnWordEntity> EnWords { get; set; }
        public DbSet<AccountEntity> Accounts { get; set; }
        public DbSet<WordsNodeEntity> WordsNodes { get; set; }
        public DbSet<TagEntity> keyWords { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StatisticClassificator>().HasMany(s => s.Statistics).WithMany(s => s.StatisticClassificators);
            modelBuilder.Entity<WordsNodeEntity>().HasMany(s => s.EnWords);
            modelBuilder.Entity<AccountEntity>().HasMany(s => s.wordsNodes).WithOne(s => s.Account);
        }

    }
}
