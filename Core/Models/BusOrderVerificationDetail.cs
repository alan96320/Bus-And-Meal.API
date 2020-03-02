using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrderVerificationDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public BusOrderVerification BusOrderVerification { get; set; }
    public int? BusOrderVerificationId { get; set; }
    public BusTime BusTime { get; set; }
    public int? BusTimeId { get; set; }
    public DormitoryBlock DormitoryBlock { get; set; }
    public int? DormitoryBlockId { get; set; }
    public int SumOrderQty { get; set; }
  }
}