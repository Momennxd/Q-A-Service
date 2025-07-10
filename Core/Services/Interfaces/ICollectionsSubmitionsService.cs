using Core.DTOs.Collections;
using Data.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Collections.CollectionsSubmissionsDTOs;

namespace Core.Services.Interfaces
{
    public interface ICollectionsSubmitionsService
    {
        public Task<int> AddSubmission(int CollectionId, int UserID);
        public Task<bool> DeleteSubmition(int SubmitionID, int UserID);


        public Task<List<SendCollectionSubmissionThumbDTO?>> GetSubmition(int collectionID, int UserID);
    }
}
