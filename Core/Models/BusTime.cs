using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusTime
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Time { get; set; }
    public int DirectionEnum { get; set; }
  }
}