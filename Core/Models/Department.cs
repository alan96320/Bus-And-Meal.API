using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  [Table("Department")]
  public class Department
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]

    public string Code { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

  }
}