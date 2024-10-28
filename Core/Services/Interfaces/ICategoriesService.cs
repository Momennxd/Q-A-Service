using Core.DTOs.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Categories.CategoriesDTOs;

namespace Core.Services.Interfaces
{
    public interface ICategoriesService
    {


        public Task<List<SendCategoryDTO>> GetCategories(string categorySubName, int RowCount);


        public Task<SendCategoryDTO> AddCategory(CreateCategoryDTO createCategoryDTO);










    }
}
