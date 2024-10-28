using Core.Services.Concrete;
using Core.Services.Interfaces;
using Data.DatabaseContext;
using Data.models.Base;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.People_Repo;
using Microsoft.Extensions.Logging;



namespace Core.Unit_Of_Work
{
    public class UnitOfWork<T, U> :
        IAsyncDisposable, IUnitOfWork<T, U>  where T : IRepository<U> where U : class, IBaseEntity<U>
    {


      

        public T EntityRepo { get; set; }

        private AppDbContext _appDbContext;






        public UnitOfWork(AppDbContext context, T Er)
        {
            _appDbContext = context;

            EntityRepo = Er;
        }



        public async Task<int> CompleteAsync()
        {
            return await _appDbContext.SaveChangesAsync(new CancellationToken());
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken)
        {
            return await _appDbContext.SaveChangesAsync(cancellationToken);
        }


        public async ValueTask DisposeAsync()
        {
            await _appDbContext.DisposeAsync();
        }


    }
}
