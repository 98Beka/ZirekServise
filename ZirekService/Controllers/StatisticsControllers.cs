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
//[Authorize(Roles = RoleService.AdminRole)]
public class StatisticController : Controller
{
    readonly ApplicationDbContext _context;
    public StatisticController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("/GetStatisticClassificators")]
    public List<StatisticClassificator> GetStatisticClassificators() =>
         _context.StatisticClassificators.ToList();

    [HttpPost("/GetStatistic")]
    public IActionResult GetStatistic(StatisticFilter filter) {
        if (string.IsNullOrEmpty(filter.StatisticClassificatorName))
            return null;

        var classificator = _context.StatisticClassificators
            .Where(s => s.Name.ToLower() == filter.StatisticClassificatorName.ToLower())
            .FirstOrDefault();

        var Statistics = _context.Statistics.Where(s => s.StatisticClassificators.Contains(classificator));

        if (filter.dateFrom != null)
            Statistics = Statistics.Where(s => s.CreatedDate >= filter.dateFrom);

        if (filter.dateTo != null)
            Statistics = Statistics.Where(s => s.CreatedDate <= filter.dateTo);

        var res = Statistics.Select(s => new { Value = s.Value, TxtValue = s.TxtValue, CreatedDate = s.CreatedDate }).ToList();

        return Ok(res);
    }
}
