using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusOrderVerificationResource
  {
    public string OrderNo { get; set; }
    public DateTime Orderdate { get; set; }
    public bool IsClosed { get; set; }
    public IList<int> OrderList { get; set; }
    public bool isUpdate { get; set; } = false;

    public ICollection<SaveBusOrderVerificationDetailResource> BusOrderVerificationDetails { get; set; }
      = new Collection<SaveBusOrderVerificationDetailResource>();
  }

  public class ViewBusOrderVerificationResource
  {
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime Orderdate { get; set; }
    public bool IsClosed { get; set; }

    public ICollection<ViewBusOrderVerificationDetailResource> BusOrderVerificationDetails { get; set; }
      = new Collection<ViewBusOrderVerificationDetailResource>();

    public ICollection<ViewBusOrderResource> BusOrders { get; set; } = new Collection<ViewBusOrderResource>();

  }
}