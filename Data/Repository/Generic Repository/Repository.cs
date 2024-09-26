using Core;
using Core_Layer.AppDbContext;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : IBaseEntity
    {


        private AppDbContext _appDbContext;

        //private ILogger _Logger;

        DbSet<IBaseEntity> _dbSet;



        public  Repository(AppDbContext context)
        {
            //_Logger = Logger;
            _appDbContext = context;
            _dbSet = context.Set<IBaseEntity>();

        }


        public virtual async Task<T?> FindAsync(dynamic ItemPK)
        {

            return await _dbSet.FindAsync(ItemPK);

        }



        public virtual async Task<List<IBaseEntity>?> GetAllItemsAsync()
        {
            return await _dbSet.ToListAsync();
        }



        public virtual async Task<EntityEntry> AddItemAsync(T Item)
        {
            return await _dbSet.AddAsync(Item);
        }


        public virtual async Task UpdateItemAsync(dynamic Id, T UpdatedItem)
        {
            // Find the existing item in the database
            T? item = await _dbSet.FindAsync(Id);

            if (item == null)
            {
                // Optionally, handle the case where the item is not found
                throw new ArgumentException($"Item with Id {Id} not found");
            }

            // Update the existing entity's values with the new data
            _appDbContext.Entry(item).CurrentValues.SetValues(UpdatedItem);

        }


        public async virtual Task DeleteItemAsync(dynamic ItemPK)
        {
            T? item = await _dbSet.FindAsync(ItemPK);

            _appDbContext.Remove(item);
        }



       


    }
}