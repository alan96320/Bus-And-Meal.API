using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class UserModuleRights
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int ModuleRightsId { get; set; }
    public ModuleRights ModuleRights { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public bool Read { get; set; }
    public bool Write { get; set; }
    public ICollection<ModuleRights> ModuleRight { get; set; }

  }
}