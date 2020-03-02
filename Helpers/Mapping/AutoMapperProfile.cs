using System.Reflection;
using AutoMapper;
using BusMeal.API.Controllers.Resources;
using BusMeal.API.Core.Models;

namespace BusMeal.API.Helpers.Mapping
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Department, ViewDepartmentResource>();
      CreateMap<Employee, ViewEmployeeResource>();
      CreateMap<User, ViewUserResource>();
      CreateMap<UserDepartment, ViewUserDepartmentResource>();
      CreateMap<AppConfiguration, ViewConfigurationResource>();
      CreateMap<ModuleRights, ViewModuleRightsResource>();
      CreateMap<UserModuleRights, ViewUserModuleRightsResource>();
      CreateMap<DormitoryBlock, ViewDormitoryBlockResource>();
      CreateMap<BusTime, ViewBusTimeResource>();
      CreateMap<MealType, ViewMealTypeResource>();
      CreateMap<MealVendor, ViewMealVendorResource>();
      CreateMap<Counter, ViewCounterResource>();
      CreateMap<Audit, ViewAuditResource>();
      CreateMap<MealOrder, ViewMealOrderResource>();
      CreateMap<MealOrderDetail, ViewMealOrderDetailResource>();
      CreateMap<MealOrderVerification, ViewMealOrderVerificationResource>();
      CreateMap<MealOrderVerificationDetail, ViewMealOrderVerificationDetailResource>();
      CreateMap<BusOrder, ViewBusOrderResource>();
      CreateMap<BusOrderDetail, ViewBusOrderDetailResource>();
      CreateMap<BusOrderVerification, ViewBusOrderVerificationResource>();
      CreateMap<BusOrderVerificationDetail, ViewBusOrderVerificationDetailResource>();

      CreateMap<SaveDepartmentResource, Department>();
      CreateMap<SaveEmployeeResource, Employee>();
      CreateMap<SaveUserResource, User>();
      CreateMap<SaveUserDepartmentResource, UserDepartment>();
      CreateMap<SaveConfigurationResource, AppConfiguration>();
      CreateMap<SaveModuleRightsResource, ModuleRights>();
      CreateMap<SaveUserModuleRightsResource, UserModuleRights>();
      CreateMap<SaveDormitoryBlockResource, DormitoryBlock>();
      CreateMap<SaveBusTimeResource, BusTime>();
      CreateMap<SaveMealTypeResource, MealType>();
      CreateMap<SaveMealVendorResource, MealVendor>();
      CreateMap<SaveCounterResource, Counter>();
      CreateMap<SaveAuditResource, Audit>();
      CreateMap<SaveMealOrderResource, MealOrder>();
      CreateMap<SaveMealOrderDetailResource, MealOrderDetail>();
      CreateMap<SaveMealOrderVerificationResource, MealOrderVerification>();
      CreateMap<SaveMealOrderVerificationDetailResource, MealOrderVerificationDetail>();
      CreateMap<SaveBusOrderResource, BusOrder>();
      CreateMap<SaveBusOrderDetailResource, BusOrderDetail>();
      CreateMap<SaveBusOrderVerificationResource, BusOrderVerification>();
      CreateMap<SaveBusOrderVerificationDetailResource, BusOrderVerificationDetail>();

    }
  }
}