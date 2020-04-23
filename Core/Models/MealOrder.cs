using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrder
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public DateTime OrderEntryDate { get; set; }
    public Department Department { get; set; }
    public int? DepartmentId { get; set; }
    public MealOrderVerification MealOrderVerification { get; set; }
    public int? MealOrderVerificationId { get; set; }
    public bool IsReadyToCollect {get;set;}
    public User User {get;set;}
    public int UserId {get;set;}
    public ICollection<MealOrderDetail> MealOrderDetails { get; set; }
      = new Collection<MealOrderDetail>();
  }
}