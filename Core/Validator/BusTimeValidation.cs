using System.Data;
using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class BusTimeValidation : AbstractValidator<SaveBusTimeResource>
  {
    private readonly DataContext context;

    public BusTimeValidation(DataContext context)
    {
      this.context = context;

      RuleFor(b => b.Code)
          .NotEmpty().WithMessage("Bus time code is required")
          .Length(3, 3).WithMessage("Bus time code length must be 3 character")
          .Matches("^[0-9]*$").WithMessage("Code must be a number");

      RuleFor(b => b.Time)
          .NotEmpty().WithMessage("Time is required")
          .Length(5, 5).WithMessage("Time length must be 5 character");

      RuleFor(b => b)
          .Must(b => !IsCodeDuplicate(b)).WithName("Code").WithMessage("Bus time code must be unique");
    }

    // TODO : Check for better technics
    private bool IsCodeDuplicate(SaveBusTimeResource resource)
    {
      if (!string.IsNullOrEmpty(resource.Code))
      {
        if (resource.isUpdate == true)
        {
          return false;
        }
        return context.BusTime.Any(b => b.Code == resource.Code);
      }
      return false;
    }
  }
}