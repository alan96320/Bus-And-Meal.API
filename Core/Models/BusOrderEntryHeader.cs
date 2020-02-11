using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrderEntryHeader
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }
    public Department Department { get; set; }
    public int? DepartmentId { get; set; }
    public DormitoryBlock DormitoryBlock { get; set; }
    public int? DormitoryBlockId { get; set; }
    public BusOrderVerificationHeader BusOrderVerificationHeader { get; set; }
    public int? BusOrderVerificationHeaderId { get; set; }
  }
}