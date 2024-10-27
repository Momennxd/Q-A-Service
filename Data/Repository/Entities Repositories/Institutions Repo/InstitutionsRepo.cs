using Data.DatabaseContext;
using Data.models.People;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Institutions_Repo
{
    public class InstitutionsRepo : Repository<Institution>, IInstitutionsRepo
    {


        private readonly AppDbContext _appDbContext;
        public InstitutionsRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }












    }
}
