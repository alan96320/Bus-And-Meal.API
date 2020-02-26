using System;
using System.Collections.Generic;

namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusVerificationResource
  {
    public string OrderNo { get; set; }
    public DateTime Orderdate { get; set; }
    public bool OrderStatus { get; set; }

    public ICollection<SaveBusVerificationDetailResource> BusOrderVerificationDetail { get; set; }
  }
}