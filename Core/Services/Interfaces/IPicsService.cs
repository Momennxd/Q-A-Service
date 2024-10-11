using Core.DTOs.Pictures;

namespace Core.Services.Interfaces
{
    public interface IPicsService
    {


        public Task<PicsDTOs.SendPicDTOs> CreatePicAsync(PicsDTOs.CreatePicDTOs createPicDTO);










    }
}
