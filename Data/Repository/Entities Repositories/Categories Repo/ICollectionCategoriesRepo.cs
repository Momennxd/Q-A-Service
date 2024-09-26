using Core_Layer.models.Collections;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Categories_Repo
{
    public interface ICollectionCategoriesRepo : IRepository<CollectionsCategories>
    {


        public Task<IEnumerable<CollectionsCategories>> GetAllByCollectionIDAsync(int CollectionID);











    }
}
