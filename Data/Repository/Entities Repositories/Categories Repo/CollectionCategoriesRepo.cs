using AutoMapper;
using Core_Layer.AppDbContext;
using Core_Layer.models.Collections;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Categories_Repo
{
    public class CollectionCategoriesRepo : Repository<CollectionsCategories>, ICollectionCategoriesRepo
    {


        AppDbContext _appDbContext;
       //ILogger _logger;


        public CollectionCategoriesRepo(ILogger Logger, AppDbContext context) : base(context)
        {
            _appDbContext = context;
           // _logger = Logger;
        }

        public async Task<IEnumerable<CollectionsCategories>> GetAllByCollectionIDAsync(int CollectionID)
        {
            return await _appDbContext.Collections_Categories.
                Where(collCateg=> collCateg.CollectionID == CollectionID).ToListAsync();
        }


    }
}
