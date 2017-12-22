using System.ComponentModel.DataAnnotations;

namespace BookStore.Data.Models
{
    public enum Condition
    {
        Зле = 0,
        Добро = 1,
        [Display(Name = "Много добро")]
        МногоДобро = 2,
        Отлично = 3
    }
}
