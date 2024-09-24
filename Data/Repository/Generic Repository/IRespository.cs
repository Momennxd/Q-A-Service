using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public interface IRespository<T> where T : class
    {


        public Task<T?> FindAsync(dynamic ItemPK);


        public Task<List<T>?> GetAllItemsAsync();


        public Task AddItemAsync(T Item);


        public Task UpdateItemAsync(dynamic Id, T UpdatedItem);


        public Task DeleteItemAsync(T ItemPK);


        public T PatchItem(JsonPatchDocument<T> NewItem, dynamic ItemPK);

    }
}
