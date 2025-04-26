using Data.models.Institutions;
using Data.models.People;
using Data.models.Questions;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Institutions_Repo
{
    public interface IInstitutionsRepo : IRepository<Institution>
    {



        public Task<Institution?> GetInstitutionAsync(int InstID);









    }
}
