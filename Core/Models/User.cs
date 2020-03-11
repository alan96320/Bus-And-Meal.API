using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class User
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string Username { get; set; }

    [Column(TypeName = "varchar(255)")]
    public byte[] PasswordHash { get; set; }

    [Column(TypeName = "varchar(255)")]
    public byte[] PasswordSalt { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string FirstName { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string LastName { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string FullName { get; set; }

    [Column(TypeName = "varchar(100)")]
    public string GddbId { get; set; }
    public bool AdminStatus { get; set; }
    public int LockTransStatus { get; set; }
    public ICollection<UserModuleRight> UserModuleRights { get; set; } = new Collection<UserModuleRight>();
    public ICollection<UserDepartment> UserDepartments { get; set; } = new Collection<UserDepartment>();
  }
}