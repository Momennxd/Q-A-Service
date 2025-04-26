using Data.DatabaseContext;
using Data.models.Institutions;
using Data.models.People;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        public async Task<Institution?> GetInstitutionAsync(int InstID)
        {
            var inst = await _appDbContext.Institutions
                        .Include(o => o.User)
                        .ThenInclude(o => o.Person)
                        .FirstOrDefaultAsync(o => o.InstitutionID == InstID);
            return inst;
        }

        public async Task<Institution?> GetInstitutionByUserIDAsync(int UserID)
        {
            return await _appDbContext.Institutions.FirstOrDefaultAsync(inst => inst.UserID == UserID);
        }
    }
}
