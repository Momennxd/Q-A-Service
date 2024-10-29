using AutoMapper;
using Core.DTOs.Categories;
using Core.DTOs.nsCategories;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.nsCategories;
using Data.models.Questions;
using Data.Repository.Entities_Repositories.Categories_Repo;
using Data.Repository.Entities_Repositories.Questions_Repo.nsQuestions_Categories;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.nsCategories.QuestionsCategoriesDTOs;

namespace Core.Services.Concrete.nsCategories
{
    public class QuestionsCategoriesService : IQuestionsCategoriesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<IQuestionsCategoriesRepo, Questions_Categories> _uowQuesCategories;

        public QuestionsCategoriesService(IMapper mapper,
            IUnitOfWork<IQuestionsCategoriesRepo, Questions_Categories> uowQuesCategories)
        {
            _uowQuesCategories = uowQuesCategories;
            _mapper = mapper;
        }


        public async Task<int> AddQuestionCategoryAsync(
            List<CreateQuestionsCategoryDTO> createQuestionsCategoryDTOs, int QuestionID)
        {

            var set = new HashSet<int>();
            var catEntities = new List<Questions_Categories>();

            foreach (var category in createQuestionsCategoryDTOs)
            {
                if (set.Contains(category.CategoryID)) continue;

                var catEntity = _mapper.Map<Questions_Categories>(category);
                catEntity.QuestionID = QuestionID;
                catEntities.Add(catEntity);

                set.Add(category.CategoryID);
            }

            await _uowQuesCategories.EntityRepo.AddItemsAsync(catEntities);

            return await _uowQuesCategories.CompleteAsync(); 

        }

        public async Task<int> DeleteQuestionCategoriesAsync(int QuestionID)
        {
            await _uowQuesCategories.EntityRepo.DeleteQuestionCategoriesAsync(QuestionID);

            return await _uowQuesCategories.CompleteAsync();
        }

        public async Task<bool> DeleteQuestionCategoryAsync(int Quesiton_CategoryID)
        {
            await _uowQuesCategories.EntityRepo.DeleteItemAsync(Quesiton_CategoryID);

            return await _uowQuesCategories.CompleteAsync() > 0;
        }

        public async Task<SendQuestionsCategoryDTO> FindAsync(int Quesiton_CategoryID)
        {
            return _mapper.Map<SendQuestionsCategoryDTO>
                (await _uowQuesCategories.EntityRepo.FindAsync(Quesiton_CategoryID));
        }

        public async Task<List<SendQuestionsCategoryDTO>> GetQuestionCategoriesAsync(int QuestionID)
        {

            var categories = await _uowQuesCategories.EntityRepo.GetQuestionCategoriesAsync(QuestionID);


            var output = new List<SendQuestionsCategoryDTO>();

            foreach (var category in categories)
            {
                output.Add(_mapper.Map<SendQuestionsCategoryDTO>(category));
            }


            return output;
        }
    }
}
