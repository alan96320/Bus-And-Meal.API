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
      CreateMap<MealType, ViewMealTypeResource>();

      CreateMap<SaveDepartmentResource, Department>();
      CreateMap<SaveEmployeeResource, Employee>();
      CreateMap<SaveUserResource, User>();
      CreateMap<SaveUserDepartmentResource, UserDepartment>();
      CreateMap<SaveConfigurationResource, AppConfiguration>();
      CreateMap<SaveModuleRightsResource, ModuleRights>();
      CreateMap<SaveMealTypeResource, MealType>();

    }
  }
}