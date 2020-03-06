using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class MealTypeValidation : AbstractValidator<SaveMealTypeResource>
  {
    private readonly DataContext context;

    public MealTypeValidation(DataContext context)
    {
      this.context = context;

      RuleFor(m => m.Code)
        .NotEmpty().WithMessage("Meal type code is required")
        .Length(3, 3).WithMessage("Meal type code length must be 3 character")
        .Must(m => !IsCodeDuplicate(m)).WithMessage("Meal type code must be unique");

      RuleFor(m => m.Name)
        .NotEmpty().WithMessage("Meal type name is required")
        .Length(2, 50).WithMessage("Meal type name length must be between 2 to 50 character");

      RuleFor(m => m.MealVendorId)
        .Must(m => !IsVendorIdDuplicate(m)).WithMessage("Vendor Id must be unique");
    }

    // TODO : Check for better technics
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