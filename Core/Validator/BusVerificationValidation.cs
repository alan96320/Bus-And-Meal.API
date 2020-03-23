using System.Linq;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Persistence;
using FluentValidation;

namespace BusMeal.API.Core.Validator
{
  public class BusVerificationValidation : AbstractValidator<SaveBusOrderVerificationResource>
  {
    private DataContext context;

    public BusVerificationValidation(DataContext context)
    {
      this.context = context;

      RuleFor(bv => bv.OrderNo)
          .NotEmpty().WithMessage("Order number is required");

      RuleFor(bv => bv.Orderdate)
          .NotEmpty().WithMessage("Date is required");

      RuleFor(bv => bv)
          .Must(bv => !IsDateDuplicate(bv)).WithName("Date").WithMessage("Date must be unique");
    }

    private bool IsDateDuplicate(SaveBusOrderVerificationResource resource)
    {
      if (!string.IsNullOrEmpty((resource.Orderdate).ToString()))
      {
        if (resource.isUpdate == true)
        {
          return false;
        }
        return context.BusOrderVerification.Any(mv => mv.Orderdate.Date == resource.Orderdate.Date);
      }
      return false;
    }
  }
}