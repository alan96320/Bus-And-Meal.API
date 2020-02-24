using System;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusOrderResource
  {
    public DateTime OrderEntryDate { get; set; }

    public int? DepartmentId { get; set; }

    public int? DormitoryBlockId { get; set; }

    public int? BusOrderVerificationHeaderId { get; set; }

    public Collection<SaveBusOrderDetailResource> BusOrderDetail { get; set; }
  }
}