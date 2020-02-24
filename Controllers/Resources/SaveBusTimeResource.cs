using System;

namespace BusMeal.API.Controllers.Resources
{
    public class SaveBusTimeResource
    {
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public int DirectionEnum { get; set; }
    }
}