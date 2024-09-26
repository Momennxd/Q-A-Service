using Core_Layer.AppDbContext;
using Core_Layer.models.Collections;
using Data.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.models.People;
using Data.models._SP_;

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


        public async Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID, bool IsPublic)
        {

            return await _appDbContext.QCollections
        .Where(coll => coll.CreatedByUserId == UserID && coll.IsPublic == IsPublic && !coll.IsDeleted)
            
        .ToListAsync(); 

        
        }


        public async Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID)
        {
            return await _appDbContext.QCollections.
                Where(coll => coll.CreatedByUserId == UserID && !coll.IsDeleted).ToListAsync();
        }

        public async Task<ICollection<string>> GetAllCategoriesAsync(int collectionId)
        {
            // Execute the stored procedure and map results to the DTO
            var categories = await _appDbContext.Set<SPCollectionCetagory>()
                .FromSqlRaw("EXEC SP_GetCategoriesNamesByCollectionId @CollectionId",
                             new SqlParameter("@CollectionId", collectionId))
                .ToListAsync();

            // Convert the list of DTOs to a list of strings
            return categories.Select(c => c.CategoryName).ToList();
        }





    }
}
