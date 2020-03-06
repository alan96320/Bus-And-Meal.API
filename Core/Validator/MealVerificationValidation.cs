using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class MealVerificationValidation : AbstractValidator<SaveMealOrderVerificationResource>
  {
    private readonly DataContext context;

    public MealVerificationValidation(DataContext context)
    {
      this.context = context;

      RuleFor(mv => mv.OrderDate)
          .NotEmpty().WithMessage("Date is required");

      RuleFor(mv => mv)
          .Must(mv => !IsDateDuplicate(mv)).WithMessage("Date can't be duplicated");
    }

    // TODO : Check for better technics
    private bool IsDateDuplicate(SaveMealOrderVerificationResource resource)
    {
      if (!string.IsNullOrEmpty((resource.OrderDate).ToString()))
      {
        return context.MealOrderVerification.Any(mv => mv.OrderDate.Date == resource.OrderDate.Date);
      }
      return false;
    }
  }
}