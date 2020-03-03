
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

      RuleFor(d => d.Name)
        .Length(1, 100).WithMessage("Name length must be between 1 to 100 character");

      RuleFor(d => d.Name)
        .NotEmpty().WithMessage("Name is required");

      RuleFor(d => d.Code)
        .Length(1, 50).WithMessage("Code length must be between 1 to 50 character");

      RuleFor(d => d.Code)
        .NotEmpty().WithMessage("Code is required");

      RuleFor(d => d.Code).Must(d => !IsDuplicate(d)).WithMessage("Department Code must be unique");
    }
    // FIXME : duplicate check
    private bool IsDuplicate(string resource)
    {
      if (!string.IsNullOrEmpty(resource))
      {
        return context.Department.Any(d => d.Code == resource);
      }
      return false;
    }
  }
}