using Core.DTOs.Categories;
using Data.models._SP_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.nsCategories.QuestionsCategoriesDTOs;

namespace Core.Services.Interfaces
{
    public interface IQuestionsCategoriesService
    {



        public Task<List<SendQuestionsCategoryDTO>> GetQuestionCategoriesAsync(int QuestionID);


        public Task<int> AddQuestionCategoryAsync(
            List<CreateQuestionsCategoryDTO> createQuestionsCategoryDTOs, int QuestionID);



        /// <summary>
        /// Deletes all the mapping between categories and questions based on question id
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns>
        /// Number of affected rows.
        /// </returns>
        public Task<int> DeleteQuestionCategoriesAsync(int QuestionID);



        /// <summary>
        /// Deletes Quesiton Category based on question id
        /// </summary>
        /// <param name="QuestionID"></param>
        /// <returns>
        /// TRUE if deleted FALSE if failed
        /// </returns>
        public Task<bool> DeleteQuestionCategoryAsync(int Quesiton_CategoryID);



        public Task<SendQuestionsCategoryDTO?> FindAsync(int Quesiton_CategoryID);








    }
}
