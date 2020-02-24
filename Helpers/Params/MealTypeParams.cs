namespace BusMeal.API.Helpers.Params
{
    public class MealTypeParams : BaseParams
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int MealVendorId { get; set; }
    }
}