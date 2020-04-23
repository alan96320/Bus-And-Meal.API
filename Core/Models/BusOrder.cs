using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrder
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }
    public Department Department { get; set; }
    public int? DepartmentId { get; set; }
    public DormitoryBlock DormitoryBlock { get; set; }
    public int? DormitoryBlockId { get; set; }
    public BusOrderVerification BusOrderVerification { get; set; }
    public int? BusOrderVerificationId { get; set; }


    public bool IsReadyToCollect { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }

    public ICollection<BusOrderDetail> BusOrderDetails { get; set; }
      = new Collection<BusOrderDetail>();
  }
}