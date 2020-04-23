using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class Audit
  {
    // [Key]
    // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    // public int Id { get; set; }
    // public DateTime TrackedDate { get; set; }

    // [Column(TypeName = "varchar(100)")]
    // public string TableName { get; set; }
    // public int RowId { get; set; }
    // public int CreatedBy { get; set; }
    // public DateTime CreatedDate { get; set; }
    // public int UpdatedBy { get; set; }
    // public DateTime UpdatedDate { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string TableName { get; set; }
    public DateTime DateTime { get; set; }

    public int UserId { get; set; }

    [Column(TypeName = "varchar(255)")]
    public string KeyValues { get; set; }

    public string OldValues { get; set; }

    public string NewValues { get; set; }
  }
}