using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class EmployeeValidation : AbstractValidator<SaveEmployeeResource>
  {
    private readonly DataContext context;

    public EmployeeValidation(DataContext context)
    {
      this.context = context;

      RuleFor(e => e.HrCoreNo)
          .NotEmpty().WithMessage("HR code number is required")
          .Length(8, 25).WithMessage("HR code length must be between 8 to 25 character");
      // .Must(e => !IsHrCodeDuplicate(e)).WithMessage("HR code must be unique");

      RuleFor(e => e.Firstname)
          .NotEmpty().WithMessage("First name is required")
          .Length(3, 25).WithMessage("First name length must be between 3 to 25 character");

      RuleFor(e => e.Lastname)
          .NotEmpty().WithMessage("First name is required")
          .MaximumLength(25).WithMessage("Last name max length is only 25 character");

      RuleFor(e => e.HIDNo)
                .NotEmpty().WithMessage("HID number is required")
                .Length(10, 25).WithMessage("HID number length must be between 10 to 25 character");
      // .Must(e => !IsHIDDuplicate(e)).WithMessage("HR code must be unique");

      RuleFor(e => e.DepartmentId)
          .NotEmpty().WithMessage("Department id is required");

    }
    private bool IsHrCodeDuplicate(string resource)
    {
      if (!string.IsNullOrEmpty(resource))
      {
        return context.Employee.Any(e => e.HrCoreNo == resource);
      }
      return false;
    }

    private bool IsHIDDuplicate(string resource)
    {
      if (!string.IsNullOrEmpty(resource))
      {
        return context.Employee.Any(e => e.HIDNo == resource);
      }
      return false;
    }
  }
}