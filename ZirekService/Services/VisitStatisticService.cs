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

        private void UpdateVisitCount() {
            var today = DateTime.Today.ToUniversalTime();       
            var VisitStatisticEntity = _context.StatisticEntity.FirstOrDefault(s => s.CreatedDate == today
                && s.StatisticClassificatorId == 1);

            if (VisitStatisticEntity != null) {
                VisitStatisticEntity.Value++;
                _context.StatisticEntity.Update(VisitStatisticEntity);
            } else {
                _context.StatisticEntity.Add(new StatisticEntity() {
                    CreatedDate = today,
                    Value = 1,
                    StatisticClassificatorId = 1 //visit
                });
            }
        }


        public void SetVisitStatisticEntity(HttpContext httpContext) {
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress.ToString();
            var today = DateTime.Today.ToUniversalTime();
            var IpStatisticEntity = _context.StatisticEntity.FirstOrDefault(s =>
                s.TxtValue == remoteIpAddress.ToString() && s.StatisticClassificatorId == 2);

            if (IpStatisticEntity == null) {
                _context.StatisticEntity.Add(new StatisticEntity {
                    CreatedDate = today,
                    StatisticClassificatorId = 2,//ipAddress
                    TxtValue = remoteIpAddress.ToString()
                });
                UpdateVisitCount();
            } else if (IpStatisticEntity.CreatedDate != today) {
                UpdateVisitCount();
                IpStatisticEntity.CreatedDate = today;
                _context.StatisticEntity.Update(IpStatisticEntity);
            }
            _context.SaveChanges();
        }
    }
}
