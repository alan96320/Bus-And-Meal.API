using System;

namespace BusMeal.API.Helpers.Params
{
    public class BusTimeParams : BaseParams
    {
        public string Code { get; set; }
        public DateTime Time { get; set; }
        public int DirectionEnum { get; set; }
    }
}