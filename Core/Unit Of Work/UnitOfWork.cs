using Core.Services.Concrete;
using Core.Services.Interfaces;
using Core_Layer.AppDbContext;
using Core_Layer.models.Collections;
using Core_Layer.models.People;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Categories_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.People_Repo;
using Microsoft.Extensions.Logging;



namespace Core.Unit_Of_Work
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {


        //REPOs
        public IUserRepo Users { get;}

        public IPersonRepo People { get; }

        public ICollectionRepo Collections { get; }

        public ICollectionCategoriesRepo CollectionCategoriesRepo { get; }

        private AppDbContext _appDbContext;

        private ILogger _Logger;




        public UnitOfWork(ILogger<IUnitOfWork> logger, AppDbContext context)
        {
            _Logger = logger;

            _appDbContext = context;

            Users = new UserRepo(logger, context);
            People = new PersonRepo(logger, context);
            Collections = new CollectionRepo(logger, context);
            CollectionCategoriesRepo = new CollectionCategoriesRepo(logger, context);

        }



        public async Task<int> CompleteAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }


        public async ValueTask DisposeAsync()
        {
            await _appDbContext.DisposeAsync();
        }


    }
}
