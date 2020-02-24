namespace BusMeal.API.Controllers.Resources
{
  public class ViewMealOrderDetailResource
  {
    public int Id { get; set; }

    public int? MealOrderEntryHeaderId { get; set; }

    public int MealTypeId { get; set; }
    public int OrderQty { get; set; }
  }
}