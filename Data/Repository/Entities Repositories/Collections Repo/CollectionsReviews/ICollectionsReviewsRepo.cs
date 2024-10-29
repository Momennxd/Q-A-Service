using Data.models.Collections;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo.CollectionsReviews
{
    public interface ICollectionsReviewsRepo : IRepository<Collections_Reviews>
    {
        Task<Collections_Reviews?> FindByUserIdAndCollectionID(int UserID, int CollectionID);
        Task<List<Collections_Reviews>> GetAllCollectionReviewsAsync(int CollectionID);
    }
}
