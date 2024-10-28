using Data.models.nsCategories;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Categories_Repo
{
    public interface ICategoriesRepo : IRepository<Categories>
    {


        public Task<List<Categories>> GetCategories(string categorySubName, int RowCount);



    }
}
