using Core.DTOs.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface IChoicesPicsService
    {


        public Task<ChoicesPicsDTOs.SendChoicePicDTO?> CreateChoicePicAsync
            (ChoicesPicsDTOs.CreateChoicePicDTO createChoicePicDTO);       

        


    }
}
