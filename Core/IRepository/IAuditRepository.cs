using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
  public interface IAuditRepository
  {
    Task<Audit> GetOne(int id);
    void Add(Audit audit);
    void Remove(Audit audit);
    Task<IEnumerable<Audit>> GetAll();
    Task<PagedList<Audit>> GetPagedAudit(AuditParams auditParams);
  }
}