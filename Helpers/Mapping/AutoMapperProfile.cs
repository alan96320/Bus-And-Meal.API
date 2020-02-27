using System.Linq;
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
      CreateMap<ModuleRight, ViewModuleRightsResource>();
      CreateMap<UserModuleRight, ViewUserModuleRightsResource>();
      CreateMap<DormitoryBlock, ViewDormitoryBlockResource>();
      CreateMap<BusTime, ViewBusTimeResource>();
      CreateMap<MealType, ViewMealTypeResource>();
      CreateMap<MealVendor, ViewMealVendorResource>();
      CreateMap<Counter, ViewCounterResource>();
      CreateMap<Audit, ViewAuditResource>();
      CreateMap<MealOrderEntryHeader, ViewMealOrderResource>();
      CreateMap<MealOrderDetail, ViewMealOrderDetailResource>();
      CreateMap<MealOrderVerificationHeader, ViewMealVerificationResource>();
      CreateMap<MealOrderVerificationHeaderTotal, ViewMealVerificationTotalResource>();
      CreateMap<BusOrderEntryHeader, ViewBusOrderResource>();
      CreateMap<BusOrderEntryDetail, ViewBusOrderDetailResource>();
      CreateMap<BusOrderVerificationHeader, ViewBusVerificationResource>();
      CreateMap<BusOrderVerificationHeaderTotal, ViewBusVerificationDetailResource>();

      CreateMap<SaveDepartmentResource, Department>();
      CreateMap<SaveEmployeeResource, Employee>();
      CreateMap<SaveUserResource, User>();
      CreateMap<SaveUserDepartmentResource, UserDepartment>();
      CreateMap<SaveConfigurationResource, AppConfiguration>();
      CreateMap<SaveModuleRightsResource, ModuleRight>();
      CreateMap<SaveUserModuleRightsResource, UserModuleRight>();
      CreateMap<SaveDormitoryBlockResource, DormitoryBlock>();
      CreateMap<SaveBusTimeResource, BusTime>();
      CreateMap<SaveMealTypeResource, MealType>();
      CreateMap<SaveMealVendorResource, MealVendor>();
      CreateMap<SaveCounterResource, Counter>();
      CreateMap<SaveAuditResource, Audit>();
      CreateMap<SaveMealOrderResource, MealOrderEntryHeader>();
      CreateMap<SaveMealOrderDetailResource, MealOrderDetail>();
      CreateMap<SaveMealVerificationResource, MealOrderVerificationHeader>();
      CreateMap<SaveMealVerificationTotalResource, MealOrderVerificationHeaderTotal>();
      CreateMap<SaveBusOrderResource, BusOrderEntryHeader>();
      CreateMap<SaveBusOrderDetailResource, BusOrderEntryDetail>();
      CreateMap<SaveBusVerificationResource, BusOrderVerificationHeader>();
      CreateMap<SaveBusVerificationDetailResource, BusOrderVerificationHeaderTotal>();
    }
  }
}