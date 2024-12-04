using AutoMapper;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Collections;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsReviews;
using Data.Repository.Entities_Repositories.Collections_Repo.CollectionsSubmitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Concrete.Collections
{
    public class CollectionsSubmitionsService : ICollectionsSubmitionsService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ICollectionsSubmitionsRepo, Collections_Submitions> _unitOfWork;

        public CollectionsSubmitionsService(IMapper mapper, IUnitOfWork<ICollectionsSubmitionsRepo, Collections_Submitions> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddSubmition(int CollectionId, int UserID)
        {
            await _unitOfWork.EntityRepo.AddItemAsync(CollectionId, UserID);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteSubmition(int SubmitionID, int UserID)
        {
            var isDeleted = await _unitOfWork.EntityRepo.DeleteSubmition(SubmitionID, UserID);
            await _unitOfWork.CompleteAsync();

            return isDeleted;
        }

        public async Task<CollectionsSubmissionsDTOs.CollectionSubmissionMainDTO?> GetBySubmissionID(int SubmissionID, int UserID)
        {
            var collectionSubmissions =await _unitOfWork.EntityRepo.GetBySubmissionID(SubmissionID, UserID);
            return _mapper.Map<CollectionsSubmissionsDTOs.CollectionSubmissionMainDTO>(collectionSubmissions);
            
        }
    }
}
