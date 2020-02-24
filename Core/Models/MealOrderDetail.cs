using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrderDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public MealOrderEntryHeader MealOrderEntryHeader { get; set; }
    public int MealOrderEntryHeaderId { get; set; }
    public MealType MealType { get; set; }
    public int MealTypeId { get; set; }
    public int OrderQty { get; set; }
  }
}