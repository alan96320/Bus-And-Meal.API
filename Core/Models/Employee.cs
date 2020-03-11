using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class Employee
  {

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string HrCoreNo { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Firstname { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Lastname { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Fullname { get; set; }

    [Column(TypeName = "varchar(30)")]
    public string HIDNo { get; set; }

    [Required]
    public int? DepartmentId { get; set; }
    public Department Department { get; set; }
  }
}