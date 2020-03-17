

namespace BusMeal.API.Controllers.Resources
{
  public class SaveMealOrderDetailResource
  {
    public int? MealOrderId { get; set; }
    public int MealTypeId { get; set; }

    public int? MealVendorId { get; set; }   // dari FE harusnya null dan di refresh di dlm BE

    public int OrderQty { get; set; }
    public bool isUpdate { get; set; } = false;
  }

  public class ViewMealOrderDetailResource
  {
    public int Id { get; set; }

    public int? MealOrderId { get; set; }
    public int MealTypeId { get; set; }
    public ViewMealTypeResource MealType { get; set; }

    // MealVendorId, memang tidak dikirim ke FE, karena BE yg akan isi.

    public int OrderQty { get; set; } = 0;
  }
}