using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  [Table("Configuration")]
  public class Configuration
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int RowGrid { get; set; }
    public DateTime LockedBusOrder { get; set; }
    public DateTime LockedMealOrder { get; set; }
  }
}