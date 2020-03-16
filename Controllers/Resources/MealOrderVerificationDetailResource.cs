namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealOrderVerificationDetailResource
  {
    public int MealTypeId { get; set; }
    public int SumOrderQty { get; set; }
    public int AdjusmentQty { get; set; }
    public int VendorId { get; set; }
    public int SwipeQty { get; set; }
    public int LogBookQty { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewMealOrderVerificationDetailResource
  {
    public int Id { get; set; }
    public int MealOrderVerificationId { get; set; }
    public int MealTypeId { get; set; }
    public ViewMealTypeResource MealType { get; set; }
    public int SumOrderQty { get; set; }
    public int AdjusmentQty { get; set; }
    public int SwipeQty { get; set; }
    public int LogBookQty { get; set; }
  }
}