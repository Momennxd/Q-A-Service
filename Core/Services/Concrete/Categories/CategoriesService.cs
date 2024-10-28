using AutoMapper;
using Core.DTOs.Categories;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.nsCategories;
using Data.Repository.Entities_Repositories.Categories_Repo;
using Data.Repository.Entities_Repositories.Collections_Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.DTOs.Categories.CategoriesDTOs;

namespace Core.Services.Concrete.Categories
{
    public class CategoriesService : ICategoriesService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ICategoriesRepo, Data.models.nsCategories.Categories > _uowCategories;

        public CategoriesService(IMapper mapper, 
            IUnitOfWork<ICategoriesRepo, Data.models.nsCategories.Categories> uowCategories)
        {
            _uowCategories = uowCategories;
            _mapper = mapper;
        }


        public async Task<List<SendCategoryDTO>> GetCategories(string categorySubName, int RowCount)
        {
            var lstCategories = await _uowCategories.EntityRepo.GetCategories(categorySubName, RowCount);

            var output  = new List<SendCategoryDTO>();

            foreach (var categ in lstCategories)
            {
                output.Add(_mapper.Map<SendCategoryDTO>(categ));
            }

            return output;
        }



    }
}
