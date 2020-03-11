namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusOrderDetailResource
  {
    public int BusOrderId { get; set; }
    public int BusTimeId {get;set;}
    public int OrderQty { get; set; }
  }

  public class ViewBusOrderDetailResource
  {
    public int Id { get; set; }
    public int BusOrderId { get; set; }

    public ViewBusTimeResource BusTime {get;set;} 
    public int BusTimeId {get;set;}
    public int OrderQty { get; set; }
  }
}