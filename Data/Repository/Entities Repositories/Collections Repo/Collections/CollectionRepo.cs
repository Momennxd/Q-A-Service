using Data.DatabaseContext;
using Data.models._SP_;
using Data.models.Collections;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> DeleteLikeAsync(int CollectionID, int UserID)
        {
            var collLikes = await _appDbContext.Collections_Likes.FirstOrDefaultAsync(cl=> cl.LikedUserID == UserID);
            if (collLikes ==  null)
                return false;

            _appDbContext.Collections_Likes.Remove(collLikes);
            return true;
        }

        public async Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID, bool IsPublic)
        {

            return await _appDbContext.QCollections
        .Where(coll => coll.CreatedByUserId == UserID && coll.IsPublic == IsPublic).ToListAsync();


        }


        public async Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID)
        {
            return await _appDbContext.QCollections.
                Where(coll => coll.CreatedByUserId == UserID).ToListAsync();
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
