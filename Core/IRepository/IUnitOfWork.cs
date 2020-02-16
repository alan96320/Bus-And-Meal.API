using System;
using System.Threading.Tasks;

namespace BusMeal.API.Core
{
  public interface IUnitOfWork
  {
    Task <bool> CompleteAsync();
  }
}