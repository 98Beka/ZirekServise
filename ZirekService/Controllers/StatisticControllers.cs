using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Minjust.Cdb.Api.Models;
using System.Data;
using ZirekService.Data;
using ZirekService.Models;
using ZirekService.Services;

namespace ZirekService.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize(Roles = RoleService.AdminRole)]
public class StatisticController : Controller
{
    readonly ApplicationDbContext _context;
    public StatisticController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/GetStatisticClassificators")]
    public List<StatisticClassificator> GetStatisticClassificators() =>
         _context.StatisticClassificator.ToList();

    [HttpPost("/GetStatistic")]
    public List<float> GetStatistic(StatisticFilter filter)
    {
        if (string.IsNullOrEmpty(filter.StatisticClassificatorName))
            return null;

        var classificator_id = _context.StatisticClassificator.Where(s => s.Name == filter.StatisticClassificatorName).Select(s=>s.StatisticClassificatorId).FirstOrDefault();
        var res = _context.StatisticEntity.Where(s => s.StatisticClassificatorId == classificator_id);

        if(filter.dateFrom!= null)
            res = res.Where(s => s.CreatedDate >= filter.dateFrom);

        if (filter.dateTo != null)
            res = res.Where(s => s.CreatedDate >= filter.dateTo);

        return res.Select(s => s.Value).ToList();
    }

    [HttpPost("/SetStatistic")]
    public ActionResult SetStatistic(StatisticViewModel st)
    {
        var classificator_id = _context.StatisticClassificator.Where(s => s.Name == st.statisticClassificatorName).Select(s => s.StatisticClassificatorId).FirstOrDefault();

        if (classificator_id == null)
            return NotFound();

        var statistic = new StatisticEntity
        {
            Value = st.Value,
            CreatedDate =  st.Date ?? DateTime.Today,
            StatisticClassificatorId = classificator_id
        };

        _context.StatisticEntity.Add(statistic);
        _context.SaveChanges();
        return Ok();
    }
}
