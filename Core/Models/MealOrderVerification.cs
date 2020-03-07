using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class MealOrderVerification
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column(TypeName = "varchar(10)")]
    public string OrderNo { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsClosed { get; set; }
    public ICollection<MealOrderVerificationDetail> MealOrderVerificationDetails { get; set; }
      = new Collection<MealOrderVerificationDetail>();

    public ICollection<MealOrder> MealOrders { get; set; }
        = new Collection<MealOrder>();      
  }
}