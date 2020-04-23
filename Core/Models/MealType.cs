using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealType
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string Code { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }
    // public int MealVendorId { get; set; }
    // public MealVendor MealVendor { get; set; }

    public ICollection<MealOrderDetail> MealOrderDetails { get; set; }
      = new Collection<MealOrderDetail>();
    public ICollection<MealOrderVerificationDetail> MealOrderVerificationDetails { get; set; }
      = new Collection<MealOrderVerificationDetail>();


  }
}