using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class AppConfiguration
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string LockedBusOrder { get; set; }
    [Column(TypeName = "varchar(10)")]
    public string LockedMealOrder { get; set; }
  }
}