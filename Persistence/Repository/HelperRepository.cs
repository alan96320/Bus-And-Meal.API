using System;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;

namespace BusMeal.API.Persistence.Repository
{
    public class HelperRepository : IHelperRepository
    {
        private readonly DataContext context;
    //    private readonly IConfigurationRepository appConfiguration;
    //        private readonly IBusOrderRepository busOrderRepository;

        public HelperRepository(DataContext context)
        {
            this.context = context;

        }

        public DateTime GetCurrentServerDateTime()
        {
            return DateTime.UtcNow;
        }

        public bool isLockedBusOrder()
        {
            // TODO : implement isLockedBusOrder
            throw new NotImplementedException();

        }

        public bool isLockedMealOrder()
        {
            // TODO : implement isLockedMeaOrder
            throw new NotImplementedException();
        }
    }
}