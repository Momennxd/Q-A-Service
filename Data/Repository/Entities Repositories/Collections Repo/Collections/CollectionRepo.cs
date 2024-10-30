using Data.DatabaseContext;
using Data.models._SP_;
using Data.models.Collections;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Entity;

namespace Data.Repository.Entities_Repositories.Collections_Repo
{
    public class CollectionRepo : Repository<QCollection>, ICollectionRepo
    {

        AppDbContext _appDbContext;


        public CollectionRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
            //_logger = Logger;
        }



        public async Task<int> DeleteCollectionAsync(int collectionID)
        {
            var totalRowsCountParam = new SqlParameter
            {
                ParameterName = "@TotalRowsCount",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Output
            };

            await _appDbContext.Database.ExecuteSqlRawAsync(
                "EXEC SP_DeleteCollection @CollectionID, @TotalRowsCount OUTPUT",
                new SqlParameter("@CollectionID", collectionID),
                totalRowsCountParam);

            // Retrieve the output parameter value
            return (int)totalRowsCountParam.Value;
        }






        public async Task<bool> DeleteLikeAsync(int CollectionID, int UserID)
        {
            var collLikes = await _appDbContext.Collections_Likes.FirstOrDefaultAsync(cl=> cl.LikedUserID == UserID);
            if (collLikes ==  null)
                return false;

            _appDbContext.Collections_Likes.Remove(collLikes);
            return true;
        }

        public async Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID, bool? IsPublic)
        {

            if (IsPublic == null)            
                return await _appDbContext.QCollections
                    .Where(coll => coll.CreatedByUserId == UserID).ToListAsync();
            

            return await _appDbContext.QCollections
        .Where(coll => coll.CreatedByUserId == UserID && coll.IsPublic == IsPublic).ToListAsync();


        }


        public async Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID)
        {
            return await _appDbContext.QCollections.
                Where(coll => coll.CreatedByUserId == UserID).ToListAsync();
        }




        public async Task<List<SPCollectionCetagory>> GetCollectionCategories(int CollectionID)
        {
            
            var categories = await _appDbContext.sp_CollectionCategories
                .FromSqlRaw("EXEC SP_GetCategoriesByCollectionID @CollectionID = {0}", CollectionID)
                .ToListAsync();

            return categories;
            
        }



        public async Task<Tuple<long, long>> GetLikesDislikes(int CollecID)
        {
            var result = await _appDbContext.Collections_Likes
                .Where(l => l.CollectionID == CollecID)
                .GroupBy(l => l.Like_Dislike)
                .Select(g => new { Like_Dislike = g.Key, Count = g.LongCount() })
                .ToListAsync();

            long likes = result.FirstOrDefault(r => r.Like_Dislike == true)?.Count ?? 0;
            long dislikes = result.FirstOrDefault(r => r.Like_Dislike == false)?.Count ?? 0;

            return new Tuple<long, long>(likes, dislikes);
        }

        public async Task<IEnumerable<QCollection>> GetTop20Collections()
        {
            var collections = await _appDbContext.QCollections
               .FromSqlRaw("EXEC GetTop20Collection")
               .ToListAsync();

           
            return collections;
        }
        public async Task<bool> LikeAsync(int UserId, int CollectionID, bool IsLike)
        {
            // Check if the user has already interacted with the collection
            var existingItem = await _appDbContext.Collections_Likes
                .FirstOrDefaultAsync(cl => cl.CollectionID == CollectionID && cl.LikedUserID == UserId);

            if (existingItem != null)
            {
                // Toggle the LikeDislike value
                existingItem.Like_Dislike = !existingItem.Like_Dislike;
                existingItem.LikeDate = DateTime.Now;
                _appDbContext.Collections_Likes.Update(existingItem);
            }
            else
            {
                // Insert a new record
                var NewItem = new Collections_Likes()
                {
                    CollectionID = CollectionID,
                    LikeDate = DateTime.Now,
                    Like_Dislike = IsLike,
                    LikedUserID = UserId
                };

                await _appDbContext.Collections_Likes.AddAsync(NewItem);
            }

            // Save changes and return true if successful
            return await _appDbContext.SaveChangesAsync(new CancellationToken()) > 0;
        }

    }
}
