using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMeal.API.Core.IRepository;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;
using Microsoft.EntityFrameworkCore;

namespace BusMeal.API.Persistence.Repository
{
  public class AuditRepository : IAuditRepository
  {
    private readonly DataContext context;
    public AuditRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(Audit audit)
    {
      context.Audit.Add(audit);
    }

    public async Task<IEnumerable<Audit>> GetAll()
    {
      var audit = await context.Audit.ToListAsync();
      return audit;
    }

    public async Task<Audit> GetOne(int id)
    {
      return await context.Audit.FindAsync(id);
    }

    public void Remove(Audit audit)
    {
      context.Remove(audit);
    }
    public async Task<PagedList<Audit>> GetPagedAudit(AuditParams auditParams)
    {
      var audit = context.Audit.AsQueryable();
      if (!string.IsNullOrEmpty(auditParams.TableName))
      {
        audit = audit.Where(e =>
        e.TableName.Contains(auditParams.TableName, StringComparison.OrdinalIgnoreCase));
      }

      if (auditParams.RowId > 0)
      {
        audit = audit.Where(u => u.RowId == auditParams.RowId);
      }
      if (auditParams.CreatedBy > 0)
      {
        audit = audit.Where(u => u.CreatedBy == auditParams.CreatedBy);
      }

      //name,sort
      if (auditParams.isDescending)
      {
        if (!string.IsNullOrEmpty(auditParams.OrderBy))
        {
          switch (auditParams.OrderBy.ToLower())
          {
            case "trackeddate":
              audit = audit.OrderByDescending(e => e.TrackedDate);
              break;
            case "tablename":
              audit = audit.OrderByDescending(e => e.TableName);
              break;
            case "rowid":
              audit = audit.OrderByDescending(e => e.RowId);
              break;
            case "createdby":
              audit = audit.OrderByDescending(e => e.CreatedBy);
              break;
            case "createddate":
              audit = audit.OrderByDescending(e => e.CreatedDate);
              break;
            case "updatedby":
              audit = audit.OrderByDescending(e => e.UpdatedBy);
              break;
            case "updateddate":
              audit = audit.OrderByDescending(e => e.UpdatedDate);
              break;
            default:
              audit = audit.OrderByDescending(e => e.TrackedDate);
              break;
          }
        }
        else
        {
          audit = audit.OrderByDescending(e => e.TrackedDate);
        }

      }
      else
      {
        if (!string.IsNullOrEmpty(auditParams.OrderBy))
        {
          switch (auditParams.OrderBy.ToLower())
          {
            case "trackeddate":
              audit = audit.OrderBy(e => e.TrackedDate);
              break;
            case "tablename":
              audit = audit.OrderBy(e => e.TableName);
              break;
            case "rowid":
              audit = audit.OrderBy(e => e.RowId);
              break;
            case "createdby":
              audit = audit.OrderBy(e => e.CreatedBy);
              break;
            case "createddate":
              audit = audit.OrderBy(e => e.CreatedDate);
              break;
            case "updatedby":
              audit = audit.OrderBy(e => e.UpdatedBy);
              break;
            case "updateddate":
              audit = audit.OrderBy(e => e.UpdatedDate);
              break;
            default:
              audit = audit.OrderBy(e => e.TrackedDate);
              break;
          }
        }
        else
        {
          audit = audit.OrderBy(e => e.TrackedDate);
        }
      }
      return await PagedList<Audit>
          .CreateAsync(audit, auditParams.PageNumber, auditParams.PageSize);
    }
  }
}