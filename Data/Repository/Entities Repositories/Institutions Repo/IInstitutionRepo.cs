using Data.models.Institutions;
using Data.models.People;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Data.Repository.Entities_Repositories
{
    public interface IInstitutionRepo : IRepository<Institution>
    {
    }
}
