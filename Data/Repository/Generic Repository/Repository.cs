using Core_Layer.AppDbContext;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{
    public class Repository<T> : IRespository<T> where T : class
    {


        private AppDbContext _appDbContext;

        private ILogger _Logger;

        DbSet<T> _dbSet;



        public  Repository(ILogger Logger, AppDbContext context)
        {
            _Logger = Logger;
            _appDbContext = context;
            _dbSet = context.Set<T>();

        }


        public virtual async Task<T?> FindAsync(dynamic ItemPK)
        {

            return await _dbSet.FindAsync(ItemPK);

        }



        public virtual async Task<List<T>?> GetAllItemsAsync()
        {
            return await _dbSet.ToListAsync();
        }



        public virtual async Task AddItemAsync(T Item)
        {
            await _dbSet.AddAsync(Item);
        }


        public virtual async Task UpdateItemAsync(dynamic Id, T UpdatedItem)
        {

            T? item = await _dbSet.FindAsync(Id);

            _appDbContext.Entry(item).CurrentValues.SetValues(UpdatedItem);

        }

        public virtual async Task DeleteItemAsync(T ItemPK)
        {
            T? item = await _dbSet.FindAsync(ItemPK);

            _appDbContext.Remove(item);
        }



        public virtual T PatchItem(JsonPatchDocument<T> NewItem, dynamic ItemPK)
        {


            if (NewItem == null || _appDbContext == null)
                return null;

             
            T Item = _dbSet.Find(ItemPK);

            //id does not exist
            if (Item == null)
                return null;

            NewItem.ApplyTo(Item);
            return Item;

          
           
        }


    }
}