using ZirekService.Data;
using ZirekService.Interfaces;
using ZirekService.Models;
using ZirekService.Models.Entities;

namespace ZirekService.Services
{
    public class VisitStatisticService : IVisitStatisticService
    {
        private readonly ApplicationDbContext _context;
        private const string _visitClassificatorName = "visit";
        private const string _ipAddressClassificatorName = "ipAddress";

        public VisitStatisticService(ApplicationDbContext context) {
            _context = context;
            if (_context.StatisticClassificators.Any(s => s.Name == _visitClassificatorName) == false)
                _context.StatisticClassificators.Add(new StatisticClassificator { Name = _visitClassificatorName });
            if (_context.StatisticClassificators.Any(s => s.Name == _ipAddressClassificatorName) == false)
                _context.StatisticClassificators.Add(new StatisticClassificator { Name = _ipAddressClassificatorName });
            _context.SaveChanges();
        }

        private void UpdateVisitCount() {
            var Classificator = _context.StatisticClassificators.FirstOrDefault(s => s.Name == _visitClassificatorName);
            if (Classificator == null)
                return;

            var visitStatisticEntity = _context.Statistics.Where(s =>
            s.StatisticClassificators.Contains(Classificator) && s.CreatedDate.Day == DateTime.Today.Day).FirstOrDefault();


            if (visitStatisticEntity != null) {
                visitStatisticEntity.Value++;
                _context.Statistics.Update(visitStatisticEntity);
            } else {
                var SE = new StatisticEntity() {
                    CreatedDate = DateTime.Today.ToUniversalTime(),
                    Value = 1,
                };
                _context.Statistics.Add(SE);
                SE.StatisticClassificators.Add(Classificator);
            }
            _context.SaveChanges();
        }


        public void SetVisitStatisticEntity(HttpContext httpContext) {
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress.ToString();
            var classificator = _context.StatisticClassificators.FirstOrDefault(s => s.Name == _ipAddressClassificatorName);

            if (classificator == null)
                return;

            var IpStatisticEntity = _context.Statistics.Where(s => s.TxtValue == remoteIpAddress.ToString() 
                && s.StatisticClassificators.Any(s => s.Name == _ipAddressClassificatorName)).FirstOrDefault();

            if (IpStatisticEntity == null) {
                var SE = new StatisticEntity {
                    CreatedDate = DateTime.Today.ToUniversalTime(),
                    TxtValue = remoteIpAddress.ToString(),
                };
                SE.StatisticClassificators.Add(classificator);
                _context.Statistics.Add(SE);
                _context.SaveChanges();
                UpdateVisitCount();
            } else if (IpStatisticEntity.CreatedDate.Day != DateTime.Today.ToUniversalTime().Day) {
                IpStatisticEntity.CreatedDate = DateTime.Today.ToUniversalTime();
                UpdateVisitCount();
            }

        }
    }
}

