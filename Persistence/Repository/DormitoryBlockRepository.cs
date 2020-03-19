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
  public class DormitoryBlockRepository : IDormitoryBlockRepository
  {
    private readonly DataContext context;
    public DormitoryBlockRepository(DataContext context)
    {
      this.context = context;
    }

    public void Add(DormitoryBlock dormitoryBlock)
    {
      context.DormitoryBlock.Add(dormitoryBlock);
    }


    public void Update(DormitoryBlock dormitoryBlock)
    {
      context.DormitoryBlock.Attach(dormitoryBlock);
      this.context.Entry(dormitoryBlock).State = EntityState.Modified;
    }    

    public async Task<IEnumerable<DormitoryBlock>> GetAll()
    {
      var dormitoryBlock = await context.DormitoryBlock.ToListAsync();
      return dormitoryBlock;
    }

    public async Task<DormitoryBlock> GetOne(int id)
    {
      return await context.DormitoryBlock.FindAsync(id);
    }

    public void Remove(DormitoryBlock dormitoryBlock)
    {
      context.Remove(dormitoryBlock);
    }

    public async Task<PagedList<DormitoryBlock>> GetPagedDormitoryBlock(DormitoryBlockParams dormitoryBlockParams)
    {
      var dormitoryBlock = context.DormitoryBlock.AsQueryable();

      // perlu user id untuk membatasi 
      if (!string.IsNullOrEmpty(dormitoryBlockParams.Code))
      {
        dormitoryBlock = dormitoryBlock.Where(e =>
        e.Code.Contains(dormitoryBlockParams.Code, StringComparison.OrdinalIgnoreCase));
      }

      if (!string.IsNullOrEmpty(dormitoryBlockParams.Name))
      {
        dormitoryBlock = dormitoryBlock.Where(e =>
        e.Name.Contains(dormitoryBlockParams.Name, StringComparison.OrdinalIgnoreCase));
      }

      //name,sort
      if (dormitoryBlockParams.isDescending)
      {
        if (!string.IsNullOrEmpty(dormitoryBlockParams.OrderBy))
        {
          switch (dormitoryBlockParams.OrderBy.ToLower())
          {
            case "code":
              dormitoryBlock = dormitoryBlock.OrderByDescending(e => e.Code);
              break;
            case "name":
              dormitoryBlock = dormitoryBlock.OrderByDescending(e => e.Name);
              break;
            default:
              dormitoryBlock = dormitoryBlock.OrderByDescending(e => e.Code);
              break;
          }
        }
        else
        {
          dormitoryBlock = dormitoryBlock.OrderByDescending(e => e.Code);
        }
      }
      else
      {
        if (!string.IsNullOrEmpty(dormitoryBlockParams.OrderBy))
        {
          switch (dormitoryBlockParams.OrderBy.ToLower())
          {
            case "code":
              dormitoryBlock = dormitoryBlock.OrderBy(e => e.Code);
              break;
            case "name":
              dormitoryBlock = dormitoryBlock.OrderBy(e => e.Name);
              break;
            default:
              dormitoryBlock = dormitoryBlock.OrderBy(e => e.Code);
              break;
          }
        }
        else
        {
          dormitoryBlock = dormitoryBlock.OrderBy(e => e.Code);
        }
      }
      return await PagedList<DormitoryBlock>
          .CreateAsync(dormitoryBlock, dormitoryBlockParams.PageNumber, dormitoryBlockParams.PageSize);
    }


  }
}