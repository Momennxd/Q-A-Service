using Core_Layer.AppDbContext;
using Core_Layer.models.Collections;
using Data.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo
{
    public class CollectionRepo : Repository<QCollection>, ICollectionRepo
    {

        AppDbContext _appDbContext;
        ILogger _logger;


        public CollectionRepo(ILogger Logger, AppDbContext context) : base(Logger, context)
        {
            _appDbContext = context;
            _logger = Logger;
        }

        public async Task<IEnumerable<QCollection>> GetAllByUserIDAsync(int UserID, bool IsPublic)
        {
            return await _appDbContext.QCollections.
                Where(coll => coll.CreatedByUserId == UserID && coll.IsPublic == IsPublic && !coll.IsDeleted).ToListAsync();
        }

       
    }
}
