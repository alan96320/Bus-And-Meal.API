using System;

namespace BusMeal.API.Helpers.Params
{
    public class AuditParams : BaseParams
    {
        public DateTime TrackedDate { get; set; }
        public string TableName { get; set; }
        public int RowId { get; set; } = 0 ;
        public int CreatedBy { get; set; } = 0 ;
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; } = 0 ;
        public DateTime UpdatedDate { get; set; }
    }
}