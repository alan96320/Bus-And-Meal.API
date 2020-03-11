using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class UserValidation : AbstractValidator<SaveUserResource>
  {
    private readonly DataContext context;

    public UserValidation(DataContext context)
    {
      this.context = context;

      RuleFor(u => u.GddbId)
          .Length(8, 10).WithMessage("User gddb Id length must be 8 to 10 character")
          .Must(u => !IsIdDuplicate(u)).WithMessage("User gddb Id must be unique");

      RuleFor(u => u.FirstName)
          .Length(3, 25).WithMessage("User first name length must be between 3 to 25 character");

      RuleFor(u => u.LastName)
          .MaximumLength(25).WithMessage("User last name length max is 25 character");
    }

    private bool IsIdDuplicate(string resource)
    {
      if (!string.IsNullOrEmpty(resource))
      {
        return context.User.Any(u => u.GddbId == resource);
      }
      return false;
    }
  }
}