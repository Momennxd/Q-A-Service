using Data.models.Base;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRepository<T> where T : class, IBaseEntity<T>
    {


        public Task<T?> FindAsync(dynamic ItemPK);


        public Task<List<T>?> GetAllItemsAsync();


        public Task<EntityEntry> AddItemAsync(T Item);


        public Task UpdateItemAsync(dynamic Id, T UpdatedItem);


        public Task DeleteItemAsync(dynamic ItemPK);

        public Task<T?> PatchItemAsync(JsonPatchDocument<T> NewItem, dynamic ItemPK);

    }
}
