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

      if (DateTime.Compare(auditParams.Date, new DateTime(01, 1, 1)) != 0)
      {
        audit = audit.Where(a => a.DateTime.Date == auditParams.Date.Date);
      }

      if (auditParams.UserId > 0)
      {
        audit = audit.Where(a => a.UserId == auditParams.UserId);
      }

      if (!string.IsNullOrEmpty(auditParams.TableName))
      {
        audit = audit.Where(a => a.TableName == auditParams.TableName);
      }


      //name,sort
      if (auditParams.isDescending)
      {
        if (!string.IsNullOrEmpty(auditParams.OrderBy))
        {
          switch (auditParams.OrderBy.ToLower())
          {

            case "date":
              audit = audit.OrderByDescending(a => a.DateTime);
              break;
            case "tabelname":
              audit = audit.OrderByDescending(a => a.TableName);
              break;
            case "userid":
              audit = audit.OrderByDescending(a => a.UserId);
              break;
            default:
              audit = audit.OrderByDescending(a => a.DateTime);
              break;
          }
        }
        else
        {
          audit = audit.OrderByDescending(a => a.DateTime);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(auditParams.OrderBy))
        {
          switch (auditParams.OrderBy.ToLower())
          {

            case "date":
              audit = audit.OrderBy(a => a.DateTime);
              break;
            case "tablename":
              audit = audit.OrderBy(a => a.TableName);
              break;
            case "userid":
              audit = audit.OrderBy(a => a.UserId);
              break;
            default:
              audit = audit.OrderBy(a => a.DateTime);
              break;
          }
        }
        else
        {
          audit = audit.OrderBy(a => a.DateTime);
        }
      }
      return await PagedList<Audit>
          .CreateAsync(audit, auditParams.PageNumber, auditParams.PageSize);
    }
  }
}