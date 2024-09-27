using AutoMapper;
using Core.Authorization_Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.Repository.Entities_Repositories.Collections_Repo;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization_Services.Concrete
{
    public class CollectionsAuthService : ICollectionsAuthService
    {

        private readonly IUnitOfWork<ICollectionRepo, QCollection> _unitOfWork;


        public CollectionsAuthService(IUnitOfWork<ICollectionRepo, QCollection> uowCollections)
        {
            _unitOfWork = uowCollections;
        }

        public async Task<bool> IsUserCollecOwner(int collecID, int userID)
        {
            // Await the result from FindAsync asynchronously
            var collection = await _unitOfWork.EntityRepo.FindAsync(collecID);

            // Ensure collection is not null and check if the user is the owner
            if (collection == null)
            {
                return false; // Collection not found
            }

            return collection.CreatedByUserId == userID;
        }

    }
}
