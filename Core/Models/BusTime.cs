using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusTime
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(TypeName = "varchar(5)")]
    public string Code { get; set; }
    [Column(TypeName = "varchar(10)")]
    public string Time { get; set; }
    public int DirectionEnum { get; set; }

    public ICollection<BusOrderDetail> BusOrderDetails { get; set; }
      = new Collection<BusOrderDetail>();
    public ICollection<BusOrderVerificationDetail> BusOrderVerificationDetails { get; set; }
      = new Collection<BusOrderVerificationDetail>();

  }
}