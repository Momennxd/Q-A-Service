using Data.DatabaseContext;
using Data.models.Collections;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo.CollectionsSubmitions
{
    public interface ICollectionsSubmitionsRepo : IRepository<Collections_Submitions>
    {
        Task<Collections_Submitions> AddItemAsync(int CollectionID, int UserID);

        Task<bool> DeleteSubmition(int SubmitionID, int UserID);


        public Task<List<Collections_Submitions?>> GetSubmitions(int collectionID, int UserID);


    }
}
