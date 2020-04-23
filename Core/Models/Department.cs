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

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Code { get; set; }

    [Required]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
      = new Collection<Employee>();
    public ICollection<BusOrder> BusOrders { get; set; }
      = new Collection<BusOrder>();
    public ICollection<MealOrder> MealOrders { get; set; }
      = new Collection<MealOrder>();
    public ICollection<UserDepartment> UserDepartments { get; set; } = new Collection<UserDepartment>();



  }
}