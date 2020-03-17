using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class CounterValidation : AbstractValidator<SaveCounterResource>
  {
    private readonly DataContext context;

    public CounterValidation(DataContext context)
    {
      this.context = context;

      RuleFor(c => c.Code)
          .NotEmpty().WithMessage("Counter code is required")
          .Length(3, 3).WithMessage("Counter code length must be 3 character");

      RuleFor(c => c.Name)
          .Length(5, 25).WithMessage("Counter name length must be between 5 to 25 character");

      RuleFor(c => c)
          .Must(c => !IsCodeDuplicate(c)).WithName("Code").WithMessage("Counter code must be unique");
    }

    // TODO : Check for better technics
    private bool IsCodeDuplicate(SaveCounterResource resource)
    {
      if (!string.IsNullOrEmpty(resource.Code))
      {
        if (resource.isUpdate == true)
        {
          return false;
        }
        return context.Counter.Any(c => c.Code == resource.Code);
      }
      return false;
    }
  }
}