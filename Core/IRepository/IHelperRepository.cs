using System;

namespace BusMeal.API.Core.IRepository
{
    public interface IHelperRepository
    {
        DateTime GetCurrentServerDateTime();

        bool isLockedMealOrder();
        bool isLockedBusOrder();
    }
}