using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class ModuleRight
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string Code { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Description { get; set; }
    public ICollection<UserModuleRight> UserModuleRights { get; set; } = new Collection<UserModuleRight>();
  }
}