namespace BusMeal.API.Controllers.Resources
{
    public class SaveConfigurationResource
    {
        public string LockedBusOrderStart1 { get; set; }

        public string LockedBusOrderEnd1 { get; set; }

        public string LockedBusOrderStart2 { get; set; }

        public string LockedBusOrderEnd2 { get; set; }

        public string LockedMealOrder { get; set; }
        public bool isUpdate { get; set; } = false;
    }

    public class ViewConfigurationResource
    {
        public int Id { get; set; }
        public string LockedBusOrderStart1 { get; set; }

        public string LockedBusOrderEnd1 { get; set; }

        public string LockedBusOrderStart2 { get; set; }

        public string LockedBusOrderEnd2 { get; set; }

        public string LockedMealOrder { get; set; }
    }
}