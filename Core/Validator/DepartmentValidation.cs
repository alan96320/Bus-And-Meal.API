using System;
using System.Collections.Generic;
using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Persistence;
using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;

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

      RuleFor(d => d.Code).Must(d => !IsDuplicate(d)).WithMessage("There is a duplicated data detected, please input another data");
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