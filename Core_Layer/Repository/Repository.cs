using Core_Layer.AppDbContext;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace DataAccess_Layer.Repository
{
    public abstract class Repository<T> where T : class
    {

       
        public Repository()
        {

        }




        #region Find item
        public static T? Find(dynamic ItemPK)
        {

            if (ItemPK == null)
                return null;

            try
            {
                using (DbContext context = clsService.contextFactory!.CreateDbContext())
                {
                    return context.Set<T>().Find(ItemPK);
                }
            }
            catch (Exception ex) { return null; }
        }


        public static async Task<T?> FindAsync(dynamic ItemPK)
        {

            if (ItemPK == null)
                return null;

            try
            {
                using (DbContext context = await clsService.contextFactory!.CreateDbContextAsync())
                {
                    return await context.Set<T>().FindAsync(ItemPK);
                }
            }
            catch (Exception ex)
            { return null; }
        }

        #endregion



        #region Get All Items
        public static List<T>? GetAllItems()
        {
            List<T>? AllItems = null;
            try
            {
                using (AppDbContext context = clsService.contextFactory!.CreateDbContext())
                {
                    AllItems = context.Set<T>().ToList();
                }
            }
            catch (Exception ex) { }

            return AllItems;

        }


        public static async Task<List<T>?> GetAllItemsAsync()
        {
            List<T>? AllItems = null;
            try
            {
                using (AppDbContext context = await clsService.contextFactory!.CreateDbContextAsync())
                {
                    AllItems = await context.Set<T>().ToListAsync();
                }
            }
            catch (Exception ex) { }

            return AllItems;

        }

        #endregion



        #region Add Item
        public static bool AddItem(T Item)
        {
            if (Item == null)
                return false;


            try
            {
                using (AppDbContext context = clsService.contextFactory!.CreateDbContext())
                {
                    context.Set<T>().Add(Item);
                    context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }


        public static async Task<bool> AddItemAsync(T Item)
        {
            if (Item == null)
                return false;


            try
            {
                using (AppDbContext context = await clsService.contextFactory!.CreateDbContextAsync())
                {

                    await context.Set<T>().AddAsync(Item);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }
        #endregion



        #region Update item

        public static T? UpdateItem(dynamic Id, T UpdatedItem)
        {
            if (UpdatedItem == null)
            {
                return null;
            }


            try
            {
                using (DbContext context = clsService.contextFactory!.CreateDbContext())
                {
                    T? item = context.Set<T>().Find(Id);
                    if (item == null)
                        return null;

                    context.Entry(item).CurrentValues.SetValues(UpdatedItem);
                    context.SaveChanges();
                    return item;
                }

            }
            catch (Exception ex) { return null; }
        }

        public static async Task<T?> UpdateItemAsync(dynamic Id, T UpdatedItem)
        {
            if (UpdatedItem == null)
            {
                return null;
            }


            try
            {
                using (DbContext context = await clsService.contextFactory!.CreateDbContextAsync())
                {
                    T? item = await context.Set<T>().FindAsync(Id);
                    if (item == null)
                        return null;
                    
                     context.Entry(item).CurrentValues.SetValues(UpdatedItem);
                     await context.SaveChangesAsync();
                    return item;
                }

            }
            catch (Exception ex) { return null; }
        }

        #endregion



        #region Patch item

        public static T PatchItem(JsonPatchDocument<T> NewItem, dynamic ItemPK)
        {


            if (NewItem == null || clsService.contextFactory == null)           
                return null;
            

            try
            {
                using (DbContext context = clsService.contextFactory!.CreateDbContext())
                {
                    T Item = context.Set<T>().Find(ItemPK);

                    //id does not exist
                    if (Item == null)
                        return null;

                    NewItem.ApplyTo(Item);

                    context.SaveChanges();

                    return Item;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static async Task<T?> PatchItemAsync(JsonPatchDocument<T> NewItem, dynamic ItemPK)
        {

            if (NewItem == null || clsService.contextFactory == null)
                return null;


            try
            {
                using (DbContext context = await clsService.contextFactory!.CreateDbContextAsync())
                {
                    T Item = await context.Set<T>().FindAsync(ItemPK);

                    //id does not exist
                    if (Item == null)
                        return null;

                    NewItem.ApplyTo(Item);

                   await context.SaveChangesAsync();

                    return Item;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion



        #region Delete Item
        public static bool DeleteItem(dynamic ItemPK)
        {
            try
            {
                using (AppDbContext context = clsService.contextFactory!.CreateDbContext())
                {
                    // Need To Perform
                    var Item = context.Set<T>().Find(ItemPK);
                    context.Set<T>().Remove(Item);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex) { return false; }
        }


        public static async Task<bool> DeleteItemAsync(dynamic ItemPK)
        {
            try
            {
                using (AppDbContext context = await clsService.contextFactory!.CreateDbContextAsync())
                {
                    // Need To Perform
                    var Item = await context.Set<T>().FindAsync(ItemPK);
                    context.Set<T>().Remove(Item);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { return false; }
        }
        #endregion



    }
}
