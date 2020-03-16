
using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class DepartmentValidation : AbstractValidator<SaveDepartmentResource>
  {
    private readonly DataContext context;

    public DepartmentValidation(DataContext context)
    {
      this.context = context;

      RuleFor(d => d.Code)
        .NotEmpty().WithMessage("Code is required")
        .Length(2, 25).WithMessage("Code length must be between 2 to 25 character")
        .Must(d => !IsCodeDuplicate(d)).WithMessage("Department Code must be unique");

      RuleFor(d => d.Name)
        .NotEmpty().WithMessage("Name is required")
        .Length(2, 50).WithMessage("Name length must be between 1 to 50 character");

    }

    // TODO : Check for better technics
    private bool IsCodeDuplicate(string resource)
    {
      if (!string.IsNullOrEmpty(resource))
      {
        return context.Department.Any(d => d.Code == resource);
      }
      return false;
    }
  }
}