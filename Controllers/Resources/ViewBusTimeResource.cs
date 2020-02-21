using System;

namespace BusMeal.API.Controllers.Resources
{
    public class ViewBusTimeResource
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public int DirectionEnum { get; set; }
    }
}