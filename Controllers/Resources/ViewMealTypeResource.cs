using BusMeal.API.Core.Models;

namespace BusMeal.API.Controllers.Resources
{
    public class ViewMealTypeResource
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ViewMealVendorResource MealVendor { get; set; }
    }
}