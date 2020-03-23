using System;
using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class BusOrderValidation : AbstractValidator<SaveBusOrderResource>
  {
    private readonly DataContext context;

    public BusOrderValidation(DataContext context)
    {
      this.context = context;

      RuleFor(bo => bo.OrderEntryDate)
          .NotEmpty().WithMessage("Date is required");

      RuleFor(bo => bo.DepartmentId)
          .NotEmpty().WithMessage("Department id is required");

      RuleFor(bo => bo)
          .Must(bo => !IsOrderDuplicate(bo)).WithName("DepartmentId").WithMessage("Can't create order with same department and Dormitory Block in one time");

      RuleFor(bo => bo)
          .Must(bo => !IsDateAcceptable(bo)).WithName("OrderEntryDate").WithMessage("The date you are entered look like invalid or your order have been exceeded the lock time");

      RuleFor(bo => bo)
          .Must(bo => !IsDepartmentValid(bo)).WithName("DepartmentId").WithMessage("The department you are entered is not your authorization");
    }

    // TODO : Check for better technics
    private bool IsOrderDuplicate(SaveBusOrderResource resource)
    {
      if (!string.IsNullOrEmpty((resource.DepartmentId).ToString()))
      {
        if (resource.isUpdate == true)
        {
          return false;
        }
        return context.BusOrder.Any(bo => bo.DepartmentId == resource.DepartmentId && bo.OrderEntryDate.Date == resource.OrderEntryDate.Date && bo.DormitoryBlockId == resource.DormitoryBlockId);
      }
      return false;
    }

    private bool IsDepartmentValid(SaveBusOrderResource resource)
    {
      if (!string.IsNullOrEmpty((resource.DepartmentId).ToString()))
      {
        var user = context.User.FirstOrDefault(u => u.Id == resource.UserId);
        if (user.AdminStatus == true)
        {
          return false;
        }

        if (context.UserDepartment.Any(ud => ud.UserId == resource.UserId && ud.DepartmentId == resource.DepartmentId) == true)
        {
          return false;
        }
        else
        {
          return true;
        }
      }
      return false;
    }

    private bool IsDateAcceptable(SaveBusOrderResource resource)
    {
      if (!string.IsNullOrEmpty((resource.OrderEntryDate).ToString()))
      {
        var user = context.User.FirstOrDefault(u => u.Id == resource.UserId);
        if (user.LockTransStatus == 1 || user.AdminStatus == true)
        {
          return false;
        }

        if (DateTime.Compare(resource.OrderEntryDate.Date, DateTime.Now.Date) < 0)
        {
          return true;
        }

        var AppConfig = context.AppConfiguration.FirstOrDefault();
        var timeLock = AppConfig.LockedBusOrder;
        if (DateTime.Now.TimeOfDay > TimeSpan.Parse(timeLock))
        {
          if (DateTime.Compare(resource.OrderEntryDate.Date, DateTime.Now.Date) > 0)
          {
            return false;
          }
          return true;
        }
      }
      return false;
    }
  }
}