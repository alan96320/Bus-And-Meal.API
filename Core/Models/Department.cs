using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class Department
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees {get;set;}
      = new Collection<Employee>();
    public ICollection<BusOrder> BusOrders {get;set;}
      = new Collection<BusOrder>();
    public ICollection<BusOrderVerification> BusOrderVerifications {get;set;}
      = new Collection<BusOrderVerification>();
    public ICollection<MealOrder> MealOrders {get;set;}
      = new Collection<MealOrder>();
    public ICollection<MealOrderVerification> MealOrderVerifications {get;set;}
      = new Collection<MealOrderVerification>();



  }
}