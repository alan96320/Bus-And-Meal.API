using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrderEntryHeader
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }
    public Department Department { get; set; }
    public int? DepartmentId { get; set; }
    public int MealOrderVerificationHeaderId { get; set; }
  }
}