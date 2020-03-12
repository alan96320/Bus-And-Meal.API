using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusOrderResource
  {
    public DateTime OrderEntryDate { get; set; }

    public int? DepartmentId { get; set; }

    public int? DormitoryBlockId { get; set; }

    public int? BusOrderVerificationId { get; set; }

    public bool isReadyToCollect {get;set;}
    public int UserId { get; set; }
    
    public ICollection<SaveBusOrderDetailResource> BusOrderDetails { get; set; }
      = new Collection<SaveBusOrderDetailResource>();
  }

  public class ViewBusOrderResource
  {
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }

    public int? DepartmentId { get; set; }
    public int? DormitoryBlockId { get; set; }
    public int? BusOrderVerificationId { get; set; }

    public bool IsReadyToCollect {get;set;}
    public int UserId { get; set; }
    public ICollection<ViewBusOrderDetailResource> BusOrderDetails { get; set; }
     = new Collection<ViewBusOrderDetailResource>();
  }
}