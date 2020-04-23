using System.Threading.Tasks;
using BusMeal.API.Core.IRepository;

namespace BusMeal.API.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public async Task<bool> CompleteAsync()
        {
            int saveResult = 0;
            try
            {
                saveResult = await context.SaveChangesAsync();
            }
            catch
            {
                saveResult = 0; // Fix Me : Make it more spesific with negatif value for :
                                //  catch (DbUpdateException ex)
                                //  catch (DbUpdateException ex)

            }
            return saveResult > 0;



        }
    }
}