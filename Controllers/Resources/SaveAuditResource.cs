using System;

namespace BusMeal.API.Controllers.Resources
{
    public class SaveAuditResource
    {
        public DateTime TrackedDate { get; set; }
        public string TableName { get; set; }
        public int RowId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}