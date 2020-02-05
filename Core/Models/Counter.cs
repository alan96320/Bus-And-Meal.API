using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  [Table("Counter")]
  public class Counter
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR(2)")]
    public string Code { get; set; }

    [Column(TypeName = "VARCHAR(100)")]
    public string Name { get; set; }

    [Column(TypeName = "VARCHAR(100)")]
    public string Location { get; set; }

    public int Status { get; set; }

  }
}