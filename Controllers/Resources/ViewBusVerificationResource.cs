using System;
using System.Collections.ObjectModel;

namespace BusMeal.API.Controllers.Resources
{
  public class ViewBusVerificationResource
  {
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime Orderdate { get; set; }
    public bool OrderStatus { get; set; }

    public Collection<ViewBusVerificationDetailResource> BusOrderVerificationDetail { get; set; }

  }
}