using Data.models.Collections;
using Data.Repositories;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Collections_Repo
{
    public interface ICollectionRepo : IRepository<QCollection>
    {



        /// <summary>
        /// Gets all the collections public/private by the user id who created them.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID, bool IsPublic);

        /// <summary>
        /// Gets ALL the collections by the user id who created them.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Task<ICollection<QCollection>> GetAllByUserIDAsync(int UserID);



        public Task<ICollection<string>> GetAllCategoriesAsync(int collectionId);



    }
}
