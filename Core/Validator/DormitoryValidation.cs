using System.Data;
using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class DormitoryValidation : AbstractValidator<SaveDormitoryBlockResource>
  {
    private readonly DataContext context;

    public DormitoryValidation(DataContext context)
    {
      this.context = context;

      RuleFor(d => d.Code)
          .NotEmpty().WithMessage("Dormitory code is required")
          .Length(2, 10).WithMessage("Dormitory code length must be between 2 to 10 character");

      RuleFor(d => d.Name)
          .NotEmpty().WithMessage("Dormitory name is required")
          .Length(2, 50).WithMessage("Dormitory name length must be between 2 to 50 character");

      RuleFor(d => d)
          .Must(d => !IsCodeDuplicate(d)).WithName("Code").WithMessage("Dormitory code must be unique");
    }

    // TODO : Check for better technics
    private bool IsCodeDuplicate(SaveDormitoryBlockResource resource)
    {
      if (!string.IsNullOrEmpty(resource.Code))
      {
        if (resource.isUpdate == true)
        {
          return false;
        }
        return context.DormitoryBlock.Any(d => d.Code == resource.Code);
      }
      return false;
    }
  }
}