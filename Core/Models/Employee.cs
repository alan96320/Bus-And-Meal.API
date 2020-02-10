using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  [Table("Employee")]
  public class Employee
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string HrCoreNo { get; set; }

    public string Firstname { get; set; }
    public string Lastname { get; set; }

    public string Fullname { get; set; }

    public Department Department { get; set; }
    public int? DepartmentId { get; set; }
  }
}