using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
  public class BusOrderEntryDetail
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public BusOrderEntryHeader BusOrderEntryHeader { get; set; }
    public int BusOrderEntryHeaderId { get; set; }
    public int OrderQty { get; set; }

  }
}