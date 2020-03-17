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
      CreateMap<AddUserResource, User>()
        .ForMember(u => u.UserDepartments, opt => opt.Ignore())
        .ForMember(u => u.UserModuleRights, opt => opt.Ignore());

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
      CreateMap<MealOrder, ViewMealOrderResource>();
      CreateMap<MealOrderDetail, ViewMealOrderDetailResource>();
      CreateMap<MealOrderVerification, ViewMealOrderVerificationResource>()
        .ForMember(m => m.mealVerificationDetails, opt => opt.MapFrom(m => m.MealOrderVerificationDetails))
        .ForMember(m => m.mealOrders, opt => opt.MapFrom(m => m.MealOrders));
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
      CreateMap<SaveModuleRightsResource, ModuleRight>();
      CreateMap<SaveUserModuleRightsResource, UserModuleRight>();
      CreateMap<SaveDormitoryBlockResource, DormitoryBlock>();
      CreateMap<SaveBusTimeResource, BusTime>();
      CreateMap<SaveMealTypeResource, MealType>();
      CreateMap<SaveMealVendorResource, MealVendor>();
      CreateMap<SaveCounterResource, Counter>();
      CreateMap<SaveAuditResource, Audit>();
      CreateMap<SaveMealOrderResource, MealOrder>();
      CreateMap<SaveMealOrderDetailResource, MealOrderDetail>();
      CreateMap<SaveMealOrderVerificationResource, MealOrderVerification>()
        .ReverseMap()
        .ForMember(ov => ov.OrderList, opt => opt.Ignore());


      CreateMap<SaveMealOrderVerificationDetailResource, MealOrderVerificationDetail>();
      CreateMap<SaveBusOrderResource, BusOrder>();
      CreateMap<SaveBusOrderDetailResource, BusOrderDetail>();
      CreateMap<SaveBusOrderVerificationResource, BusOrderVerification>()
              .ReverseMap()
              .ForMember(ov => ov.OrderList, opt => opt.Ignore());
      CreateMap<SaveBusOrderVerificationDetailResource, BusOrderVerificationDetail>();

    }
  }
}