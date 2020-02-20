using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.Models;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class DepartmentValidation : AbstractValidator<SaveDepartmentResource>
  {
    public DepartmentValidation()
    {
      RuleFor(d => d.Name).Length(1, 255);
      RuleFor(d => d.Code).Length(1, 255);
    }
  }
}