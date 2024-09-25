using AutoMapper;
using Core.Authorization_Services.Interfaces;
using Core.Unit_Of_Work;
using Core_Layer.AppDbContext;
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

        private readonly IUnitOfWork _unitOfWork;


        public CollectionsAuthService(ILogger<IUnitOfWork> logger, AppDbContext context)
        {
            _unitOfWork = new UnitOfWork(logger, context);
        }

        public async Task<bool> IsUserOwner(int collecID, int userID)
        {
            // Await the result from FindAsync asynchronously
            var collection = await _unitOfWork.Collections.FindAsync(collecID);

            // Ensure collection is not null and check if the user is the owner
            if (collection == null)
            {
                return false; // Collection not found
            }

            return collection.CreatedByUserId == userID;
        }

    }
}
