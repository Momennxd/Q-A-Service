using Data.models.Base;
using Data.Repositories;
using Data.Repository.Entities_Repositories.Categories_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Data.Repository.Entities_Repositories.People_Repo;
using System;
using System.Threading.Tasks;

namespace UoW.Unit_Of_Work
{
    public interface IUnitOfWork<T, U> where T : IRepository<U> where U : class, IBaseEntity<U> 
    {
        /// <summary>
        /// Represents the Entity Repository that interact with the database.
        /// </summary>
        T EntityRepo { get; set; }

        Task<int> CompleteAsync();

    }
}
