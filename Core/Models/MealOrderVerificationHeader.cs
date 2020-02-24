using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrderVerificationHeader
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public bool OrderedStatus { get; set; }
    public Collection<MealOrderVerificationHeaderTotal> MealVerificationTotal { get; set; }
  }
}