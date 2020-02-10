using AutoMapper;
using BusMeal.API.Core.Models;
using BusMeal.API.Core.Persistence.Resources;


namespace BusMeal.API.Helpers.Mapping
{
  public class AutoMapperProfile : Profile
  {
    public AutoMapperProfile()
    {
      CreateMap<Department, ViewDepartmentResource>();
      CreateMap<SaveDepartmentResource, Department>();
    }
  }
}