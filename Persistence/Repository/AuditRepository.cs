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
        audit = audit.Where(a => a.TableName.Contains(auditParams.TableName, StringComparison.OrdinalIgnoreCase));
      }

      if (auditParams.RowId > 0)
      {
        audit = audit.Where(a => a.RowId == auditParams.RowId);
      }
      if (auditParams.CreatedBy > 0)
      {
        audit = audit.Where(a => a.CreatedBy == auditParams.CreatedBy);
      }

      //name,sort
      if (auditParams.isDescending)
      {
        if (!string.IsNullOrEmpty(auditParams.OrderBy))
        {
          switch (auditParams.OrderBy.ToLower())
          {
            case "trackeddate":
              audit = audit.OrderByDescending(a => a.TrackedDate);
              break;
            case "tablename":
              audit = audit.OrderByDescending(a => a.TableName);
              break;
            case "rowid":
              audit = audit.OrderByDescending(a => a.RowId);
              break;
            case "createdby":
              audit = audit.OrderByDescending(a => a.CreatedBy);
              break;
            case "createddate":
              audit = audit.OrderByDescending(a => a.CreatedDate);
              break;
            case "updatedby":
              audit = audit.OrderByDescending(a => a.UpdatedBy);
              break;
            case "updateddate":
              audit = audit.OrderByDescending(a => a.UpdatedDate);
              break;
            default:
              audit = audit.OrderByDescending(a => a.TrackedDate);
              break;
          }
        }
        else
        {
          audit = audit.OrderByDescending(a => a.TrackedDate);
        }

      }
      else
      {
        if (!string.IsNullOrEmpty(auditParams.OrderBy))
        {
          switch (auditParams.OrderBy.ToLower())
          {
            case "trackeddate":
              audit = audit.OrderBy(a => a.TrackedDate);
              break;
            case "tablename":
              audit = audit.OrderBy(a => a.TableName);
              break;
            case "rowid":
              audit = audit.OrderBy(a => a.RowId);
              break;
            case "createdby":
              audit = audit.OrderBy(a => a.CreatedBy);
              break;
            case "createddate":
              audit = audit.OrderBy(a => a.CreatedDate);
              break;
            case "updatedby":
              audit = audit.OrderBy(a => a.UpdatedBy);
              break;
            case "updateddate":
              audit = audit.OrderBy(a => a.UpdatedDate);
              break;
            default:
              audit = audit.OrderBy(a => a.TrackedDate);
              break;
          }
        }
        else
        {
          audit = audit.OrderBy(a => a.TrackedDate);
        }
      }
      return await PagedList<Audit>
          .CreateAsync(audit, auditParams.PageNumber, auditParams.PageSize);
    }
  }
}