using System.ComponentModel.DataAnnotations;

namespace ZirekService.Models;

public class StatisticClassificator
{
    [Key]
    public int StatisticClassificatorId { get; set; }
    public string Name { get; set; }
    public List<StatisticEntity>? StatisticModels { get; set; }
}
