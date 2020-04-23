using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrderVerification
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string OrderNo { get; set; }
    public DateTime Orderdate { get; set; }
    public bool IsClosed { get; set; }
    public ICollection<BusOrder> BusOrders { get; set; }
        = new Collection<BusOrder>();

    public ICollection<BusOrderVerificationDetail> BusOrderVerificationDetails { get; set; }
        = new Collection<BusOrderVerificationDetail>();
  }
}