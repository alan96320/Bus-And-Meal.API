namespace BusMeal.API.Helpers.Params
{
    public class CounterParams : BaseParams
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Status { get; set; } = 0;
    }
}