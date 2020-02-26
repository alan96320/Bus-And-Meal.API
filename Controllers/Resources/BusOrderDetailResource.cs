namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusOrderDetailResource
  {
    public int BusOrderEntryHeaderId { get; set; }
    public int OrderQty { get; set; }
  }

  public class ViewBusOrderDetailResource
  {
    public int Id { get; set; }
    public int BusOrderEntryHeaderId { get; set; }
    public int OrderQty { get; set; }
  }
}