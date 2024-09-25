using Data.Repositories;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.People_Repo;
using System;
using System.Threading.Tasks;

namespace Core.Unit_Of_Work
{
    public interface IUnitOfWork : IDisposable
    {


        IUserRepo Users { get; }

        IPersonRepo People { get; }

        ICollectionRepo Collections { get; }

        Task<int> CompleteAsync();



    }
}
