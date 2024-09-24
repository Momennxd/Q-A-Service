using Core.Services.Concrete;
using Core_Layer.AppDbContext;
using Core_Layer.models.People;
using Data.Repositories;
using Microsoft.Extensions.Logging;



namespace Core.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepo Users { get;}


        private AppDbContext _appDbContext;

        private ILogger _Logger;




        public UnitOfWork(ILogger<IUnitOfWork> logger, AppDbContext context)
        {
            _Logger = logger;

            _appDbContext = context;

            Users = new UserRepo(logger, context);

        }



        public async Task<int> CompleteAsync()
        {
           return await _appDbContext.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _appDbContext.DisposeAsync();
        }

        
    }
}
