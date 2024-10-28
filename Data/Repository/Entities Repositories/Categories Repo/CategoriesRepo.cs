using Data.DatabaseContext;
using Data.models.nsCategories;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Categories_Repo
{
    public class CategoriesRepo : Repository<Categories>, ICategoriesRepo
    {

        private readonly AppDbContext _appDbContext;


        public CategoriesRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public async Task<List<Categories>> GetCategories(string categorySubName, int rowCount)
        {
            var categorySubNameParam = new SqlParameter("@CategorySubName", categorySubName);
            var rowCountParam = new SqlParameter("@RowsNum", rowCount);

            var categories = await _appDbContext.Categories
                .FromSqlRaw("EXEC SP_GetCategories @CategorySubName, @RowsNum", categorySubNameParam, rowCountParam)
                .ToListAsync();

            return categories;
            
        }

    }
}
