using System;
using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class MealOrderValidation : AbstractValidator<SaveMealOrderResource>
  {
    private readonly DataContext context;

    public MealOrderValidation(DataContext context)
    {
      this.context = context;

      RuleFor(mo => mo.OrderEntryDate)
          .NotEmpty().WithMessage("Date is required");

      RuleFor(mo => mo.DepartmentId)
          .NotEmpty().WithMessage("Department id is required");

      RuleFor(mo => mo)
          .Must(mo => !IsDepartmentDateUnique(mo)).WithName("DepartmentId").WithMessage("One department can't order more than one times a day");

      RuleFor(mo => mo)
          .Must(mo => !IsDateAcceptable(mo)).WithName("OrderEntryDate").WithMessage("The date you are entered look like invalid or your order have been exceeded the lock time");

      RuleFor(mo => mo)
          .Must(mo => !IsDepartmentValid(mo)).WithName("DepartmentId").WithMessage("The department you are entered is not your authorization");
    }



    private bool IsDepartmentDateUnique(SaveMealOrderResource resource)
    {
      if (!string.IsNullOrEmpty((resource.DepartmentId).ToString()))
      {
        return context.MealOrder.Any(mo => mo.DepartmentId == resource.DepartmentId && mo.OrderEntryDate.Date == resource.OrderEntryDate.Date);
      }
      return false;
    }

    private bool IsDepartmentValid(SaveMealOrderResource resource)
    {
      if (!string.IsNullOrEmpty((resource.DepartmentId).ToString()))
      {
        var user = context.User.FirstOrDefault(u => u.Id == resource.UserId);
        if (user.AdminStatus == true)
          return false;

        if (context.UserDepartment.Any(ud => ud.UserId == resource.UserId && ud.DepartmentId == resource.DepartmentId))
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

    private bool IsDateAcceptable(SaveMealOrderResource resource)
    {
      if (!string.IsNullOrEmpty((resource.OrderEntryDate).ToString()))
      {
        if (DateTime.Compare(resource.OrderEntryDate.Date, DateTime.Now.Date) < 0)
        {
          return true;
        }

        var AppConfig = context.AppConfiguration.FirstOrDefault();
        var timeLock = AppConfig.LockedMealOrder;
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