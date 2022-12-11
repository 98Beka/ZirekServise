namespace ZirekService.Models;

public class StatisticEntity
{
    public int Id { get; set; }
    public float Value { get; set; } = 0;
    public string TxtValue { get; set; } = String.Empty;
    public DateTime CreatedDate { get; set; }
    public int StatisticClassificatorId { get; set; }
    public StatisticClassificator StatisticClassificator { get; set; }
}
