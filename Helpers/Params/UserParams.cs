namespace BusMeal.API.Helpers.Params
{
    public class UserParams : BaseParams
    {
        public string GddbId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public bool isActive { get; set; } = true;
    }
}