using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrderVerificationHeaderTotal
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public MealOrderVerificationHeader MealOrderVerificationHeader { get; set; }
    public int MealOrderVerificationHeaderId { get; set; }
    public MealType MealType { get; set; }
    public int MealTypeId { get; set; }
    public int SumOrderQty { get; set; }
    public int AdjusmentQty { get; set; }
    public int SwipeQty { get; set; }
    public int LogBookQty { get; set; }
  }
}