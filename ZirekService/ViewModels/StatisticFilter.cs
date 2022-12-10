namespace Minjust.Cdb.Api.Models;

public class StatisticFilter
{
    public DateTime? dateFrom { get; set; }
    public DateTime? dateTo { get; set; }
    public string StatisticClassificatorName { get; set; }
}
