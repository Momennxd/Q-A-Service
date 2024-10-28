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

namespace Core.Services.Concrete.nsCategories
{
    public class CategoriesService : ICategoriesService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ICategoriesRepo, Categories > _uowCategories;

        public CategoriesService(IMapper mapper, 
            IUnitOfWork<ICategoriesRepo, Categories> uowCategories)
        {
            _uowCategories = uowCategories;
            _mapper = mapper;
        }

        public async Task<SendCategoryDTO> AddCategory(CreateCategoryDTO createCategoryDTO)
        {
            
            var entity = _mapper.Map<Categories>(createCategoryDTO);

            await _uowCategories.EntityRepo.AddItemAsync(entity);

            if (await _uowCategories.CompleteAsync() == 0) throw new ArgumentNullException();

            return _mapper.Map<SendCategoryDTO>(entity);

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
