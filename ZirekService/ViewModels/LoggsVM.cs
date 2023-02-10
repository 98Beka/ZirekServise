using System.ComponentModel.DataAnnotations;

namespace ZirekService.ViewModels;

public class LoggsVm
{
    [Display(Name = "Код")]
    public long Id { get; set; }
    [Display(Name = "Наименование")]
    public string Name { get; set; }
    [Display(Name = "Когда создан")]
    public DateTime CreatedAt { get; set; }
    [Display(Name = "Кем создан")]
    public string CreatedBy { get; set; }
}
public class LogHistoryViewModel
{
    [Display(Name = "Код")]
    public long Id { get; set; }
    [Display(Name = "Действие")]
    public byte ActionType { get; set; }
    [Display(Name = "Время")]
    public DateTime CreatedAt { get; set; }
    [Display(Name = "Пользователь")]
    public string CreatedBy { get; set; }
}
