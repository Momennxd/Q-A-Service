using Data.DatabaseContext;
using Data.models.Pictures;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Pictures.Base
{
    public class PicsRepo : Repository<models.Pictures.Pics>, IPicsRepo
    {

        private AppDbContext _appDbContext;
        public PicsRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }








    }
}
