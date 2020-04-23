using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrderDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public BusOrder BusOrder { get; set; }
    public int BusOrderId { get; set; }
    public BusTime BusTime { get; set; }
    public int BusTimeId { get; set; }
    public int OrderQty { get; set; }

  }
}