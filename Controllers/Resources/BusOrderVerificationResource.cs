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

    public ICollection<SaveBusOrderResource> BusOrders {get;set;}
      = new Collection<SaveBusOrderResource>();

    public ICollection<SaveBusOrderVerificationDetailResource> BusOrderVerificationDetails { get; set; }
      = new Collection<SaveBusOrderVerificationDetailResource>();
  }

  public class ViewBusOrderVerificationResource
  {
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime Orderdate { get; set; }
    public bool IsClosed { get; set; }

    public ICollection<ViewBusOrderResource> BusOrders {get;set;}
      = new Collection<ViewBusOrderResource>();    

    public ICollection<ViewBusOrderVerificationDetailResource> BusOrderVerificationDetails { get; set; }
      = new Collection<ViewBusOrderVerificationDetailResource>();

  }
}