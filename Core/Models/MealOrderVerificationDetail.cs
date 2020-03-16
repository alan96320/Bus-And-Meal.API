using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrderVerificationDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public MealOrderVerification MealOrderVerification { get; set; }
    public int MealOrderVerificationId { get; set; }
    public MealType MealType { get; set; }
    public int MealTypeId { get; set; }
    public int? VendorId { get; set; }
    public int SumOrderQty { get; set; }
    public int AdjusmentQty { get; set; }
    public int SwipeQty { get; set; }
    public int LogBookQty { get; set; }
  }
}