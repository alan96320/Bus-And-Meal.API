using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrderVerificationHeader
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime Orderdate { get; set; }
    public bool OrderStatus { get; set; }

    public Collection<BusOrderVerificationHeaderTotal> BusOrderVerificationDetail { get; set; }
  }
}