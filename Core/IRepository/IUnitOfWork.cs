using System;
using System.Threading.Tasks;

namespace BusMeal.API.Core.IRepository
{
  public interface IUnitOfWork
  {
    Task<bool> CompleteAsync();
  }
}