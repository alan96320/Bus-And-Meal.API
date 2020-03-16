namespace BusMeal.API.Controllers.Resources
{
  public class SaveBusOrderVerificationDetailResource
  {
    //public int BusOrderVerificationId { get; set; }
    public int BusTimeId { get; set; }
    //public int DormitoryBlockId { get; set; }
    public int SumOrderQty { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewBusOrderVerificationDetailResource
  {
    public int Id { get; set; }
    public int? BusOrderVerificationId { get; set; }

    public ViewBusTimeResource BusTime { get; set; }
    public int BusTimeId { get; set; }

    // public ViewDormitoryBlockResource DormitoryBlock {get;set;}
    // public int DormitoryBlockId { get; set; }
    public int SumOrderQty { get; set; }
  }
}
