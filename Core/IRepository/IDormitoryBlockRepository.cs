using System.Collections.Generic;
using System.Threading.Tasks;
using BusMeal.API.Core.Models;
using BusMeal.API.Helpers;
using BusMeal.API.Helpers.Params;

namespace BusMeal.API.Core.IRepository
{
    public interface IDormitoryBlockRepository
    {
        Task<DormitoryBlock> GetOne(int id);
        void Add(DormitoryBlock dormitoryBlock);
        void Remove(DormitoryBlock dormitoryBlock);
        //  void Update(Employee employee)
        Task<IEnumerable<DormitoryBlock>> GetAll();
        Task<PagedList<DormitoryBlock>> GetPagedDormitoryBlock(DormitoryBlockParams dormitoryBlockParams);
    }
}