using Core.DTOs.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICollectionCategoriesService
    {


        //Task<IEnumerable<CollectionsDTOs.SendCollectionDTO>>
        //    GetAllCollectionCategoriesAsync(int CollectionID);






        Task DeleteCollectionCategoryAsync(int id);


    }
}
