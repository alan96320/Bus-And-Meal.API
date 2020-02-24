namespace BusMeal.API.Controllers.Resources
{
  public class ViewBusVerificationDetailResource
  {
    public int Id { get; set; }
    public int? BusOrderVerificationHeaderId { get; set; }

    public int BusTimeId { get; set; }
    public int DormitoryBlockId { get; set; }
    public int SumOrderQty { get; set; }
  }
}