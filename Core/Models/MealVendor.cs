using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealVendor
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string Code { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string ContactName { get; set; }

    [Column(TypeName = "varchar(15)")]
    public string ContactPhone { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string ContactEmail { get; set; }
    public MealType MealType { get; set; }
    public int? MealTypeId { get; set; }


  }
}