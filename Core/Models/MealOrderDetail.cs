using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrderDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public MealOrder MealOrder { get; set; }
    public int? MealOrderId { get; set; }
    public MealType MealType { get; set; }
    public int? MealTypeId { get; set; }
    public int OrderQty { get; set; }
  }
}