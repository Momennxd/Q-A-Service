using Core.DTOs.Pictures;

namespace Core.Services.Interfaces
{
    public interface IPicsService
    {


        public Task<PicsDTOs.SendPicDTO> CreatePicAsync(PicsDTOs.CreatePicDTO createPicDTO);










    }
}
