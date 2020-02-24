namespace BusMeal.API.Controllers.Resources
{
  public class ViewMealVerificationTotalResource
  {
    public int Id { get; set; }
    public int MealOrderVerificationHeaderId { get; set; }
    public int MealTypeId { get; set; }
    public int SumOrderQty { get; set; }
    public int AdjusmentQty { get; set; }
    public int SwipeQty { get; set; }
    public int LogBookQty { get; set; }
  }
}