using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrderVerificationHeaderTotal
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public BusOrderVerificationHeader BusOrderVerificationHeader { get; set; }
    public int? BusOrderVerificationHeaderId { get; set; }
    public BusTime BusTime { get; set; }
    public int? BusTimeId { get; set; }
    public DormitoryBlock DormitoryBlock { get; set; }
    public int? DormitoryBlockId { get; set; }
    public int SumOrderQty { get; set; }
  }
}