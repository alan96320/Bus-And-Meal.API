using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class ModuleRights
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MyProperty { get; set; }

    public string Code { get; set; }
    public string Description { get; set; }
  }
}