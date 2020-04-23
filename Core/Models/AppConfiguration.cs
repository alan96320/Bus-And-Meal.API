using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusMeal.API.Core.Models
{
    public class AppConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string LockedBusOrderStart1 { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string LockedBusOrderEnd1 { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string LockedBusOrderStart2 { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string LockedBusOrderEnd2 { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string LockedMealOrder { get; set; }
    }
}