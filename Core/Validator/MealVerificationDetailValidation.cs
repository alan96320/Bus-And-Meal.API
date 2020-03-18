using BusMeal.API.Controllers.Resources;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class MealVerificationDetailValidation : AbstractValidator<SaveMealOrderVerificationDetailResource>
  {
    public MealVerificationDetailValidation()
    {
      RuleFor(mvd => mvd.VendorId)
          .NotEmpty().WithMessage("Vendor id is required");
    }
  }
}