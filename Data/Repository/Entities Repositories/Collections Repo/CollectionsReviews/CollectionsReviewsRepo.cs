using Data.DatabaseContext;
using Data.models.Collections;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo.CollectionsReviews
{
    public class CollectionsReviewsRepo : Repository<Collections_Reviews>, ICollectionsReviewsRepo
    {
        AppDbContext _appDbContext;

        public CollectionsReviewsRepo(AppDbContext context)
            : base(context)
        {
            _appDbContext = context;
        }

        public async Task<Collections_Reviews?> FindByUserIdAndCollectionID(int UserID, int CollectionID)
        {
            return await _appDbContext
                        .Collections_Reviews
                        .FirstOrDefaultAsync(entity=> 
                                             entity.CollectionID == CollectionID &&
                                             entity.UserID == UserID); 
        }

        public async Task<List<Collections_Reviews>> GetAllCollectionReviewsAsync(int CollectionID)
        {
            return await _appDbContext.Collections_Reviews
                        .Where(review => review.CollectionID == CollectionID)
                        .ToListAsync();
        }
    }
}
