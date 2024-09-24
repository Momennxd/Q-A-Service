using Data.Repositories;
using System;
using System.Threading.Tasks;

namespace Core.Unit_Of_Work
{
    public interface IUnitOfWork : IDisposable
    {


        IUserRepo Users { get; }

        Task<int> CompleteAsync();



    }
}
