using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class DormitoryBlock
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public ICollection<BusOrder> BusOrders {get;set;}
      = new Collection<BusOrder>();

    public ICollection<BusOrderVerificationDetail> BusOrderVerificationDetails {get;set;}
      = new Collection<BusOrderVerificationDetail>();
  }
}