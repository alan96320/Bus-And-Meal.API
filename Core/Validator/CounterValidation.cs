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
          .Length(3, 3).WithMessage("Counter code length must be 3 character")
          .Must(c => !IsCodeDuplicate(c)).WithMessage("Code must be unique");

      RuleFor(c => c.Name)
          .Length(5, 25).WithMessage("Counter name length must be between 5 to 25 character");
    }
    private bool IsCodeDuplicate(string resource)
    {
      if (!string.IsNullOrEmpty(resource))
      {
        return context.Counter.Any(c => c.Code == resource);
      }
      return false;
    }
  }
}