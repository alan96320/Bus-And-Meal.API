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
      CreateMap<SaveDepartmentResource, Department>();
      CreateMap<Employee, ViewEmployeeResource>();
      CreateMap<SaveEmployeeResource, Employee>();
    }
  }
}