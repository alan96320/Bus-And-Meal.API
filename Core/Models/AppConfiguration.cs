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
    public int RowGrid { get; set; }
    public DateTime LockedBusOrder { get; set; }
    public DateTime LockedMealOrder { get; set; }
  }
}