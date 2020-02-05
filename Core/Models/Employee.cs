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

    [Column(TypeName = "VARCHAR(8)")]
    public string HrCoreNo { get; set; }

    [Column(TypeName = "VARCHAR(100)")]
    public string Firstname { get; set; }

    [Column(TypeName = "VARCHAR(100)")]
    public string Lastname { get; set; }

    [Column(TypeName = "VARCHAR(100)")]
    public string Fullname { get; set; }

    public Department Department { get; set; }
    public int DepartmentId { get; set; }
  }
}