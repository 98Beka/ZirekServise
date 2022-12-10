using ZirekService.Data;
using ZirekService.Interfaces;
using ZirekService.Models;

namespace ZirekService.Services
{
    public class VisitStatisticService : IVisitStatisticService
    {
        private readonly ApplicationDbContext _context;
        public VisitStatisticService(ApplicationDbContext context) {
            _context = context;
        }

        public void SetVisitStatisticEntity(HttpContext httpContext) {
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress;
            var statisticEntity = _context.StatisticEntity.FirstOrDefault(s => s.CreatedDate == DateTime.Today);
            if (statisticEntity != null) {
                statisticEntity.Value += 1;
                _context.StatisticEntity.Update(statisticEntity);
            } else {
                _context.StatisticEntity.Add(new StatisticEntity() {
                    CreatedDate = DateTime.Today,
                    Value = 1,
                    StatisticClassificatorId = 1 //visit
                });
            }
            _context.SaveChanges();
        }
    }
}
