using System.ComponentModel.DataAnnotations;
using ZirekService.Models.Entities;

namespace ZirekService.Models;

public class StatisticClassificator {
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public List<StatisticEntity>? Statistics { get; set; } = new List<StatisticEntity>();
}
