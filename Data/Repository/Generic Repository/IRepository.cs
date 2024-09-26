using Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRepository<T> where T : IBaseEntity
    {


        public Task<T?> FindAsync(dynamic ItemPK);


        public Task<List<IBaseEntity>?> GetAllItemsAsync();


        public Task<EntityEntry> AddItemAsync(T Item);


        public Task UpdateItemAsync(dynamic Id, T UpdatedItem);


        public Task DeleteItemAsync(dynamic ItemPK);


    }
}
