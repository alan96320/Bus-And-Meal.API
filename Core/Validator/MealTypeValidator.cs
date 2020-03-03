using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class MealTypeValidator : AbstractValidator<SaveMealTypeResource>
  {
    private readonly DataContext context;

    public MealTypeValidator(DataContext context)
    {
      this.context = context;

      RuleFor(m => m.Code).Must(m => !IsCodeDuplicate(m)).WithMessage("Meal Type code must be unique");

      RuleFor(m => m.MealVendorId).Must(m => !IsVendorIdDuplicate(m)).WithMessage("Vendor Id must be unique");

    }
    // FIXME : check duplicate value
    private bool IsCodeDuplicate(string resource)
    {
      if (!string.IsNullOrEmpty(resource))
      {
        return context.MealType.Any(d => d.Code == resource);
      }
      return false;
    }

    private bool IsVendorIdDuplicate(int resource)
    {
      if (!string.IsNullOrEmpty(resource.ToString()))
      {
        return context.MealType.Any(d => d.MealVendorId == resource);
      }
      return false;
    }
  }
}