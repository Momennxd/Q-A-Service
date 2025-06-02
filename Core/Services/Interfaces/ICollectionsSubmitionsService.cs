using Core.DTOs.Collections;
using Data.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICollectionsSubmitionsService
    {
        public Task<int> AddSubmission(int CollectionId, int UserID);
        public Task<bool> DeleteSubmition(int SubmitionID, int UserID);


        public Task<CollectionsSubmissionsDTOs.CollectionSubmissionMainDTO?> GetBySubmissionID(int SubmitionID, int UserID);
    }
}
