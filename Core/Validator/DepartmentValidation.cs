using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.Models;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class DepartmentValidation : AbstractValidator<SaveDepartmentResource>
  {
    public DepartmentValidation()
    {
      RuleFor(d => d.Name).Length(1, 100).WithMessage("Panjang karakter tidak boleh kurang dari 1 dan tidak lebih dari 100");
      RuleFor(d => d.Code).Length(1, 50).WithMessage("Panjang karakter tidak boleh kurang dari 1 dan tidak lebih dari 50");
    }
  }
}