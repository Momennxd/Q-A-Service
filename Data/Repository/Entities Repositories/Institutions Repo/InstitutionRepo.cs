using Data.DatabaseContext;
using Data.models.Institutions;
using Data.Repositories;

namespace Data.Repository.Entities_Repositories
{
    public class InstitutionRepo : Repository<Institution>, IInstitutionRepo
    {
        AppDbContext _context;
        public InstitutionRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
