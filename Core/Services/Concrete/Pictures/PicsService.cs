using AutoMapper;
using Core.DTOs.Pictures;
using Core.Services.Interfaces;
using Core.Unit_Of_Work;
using Data.models.Pictures;
using Data.Repository.Entities_Repositories.Pictures.Base;
using Services.Cloudinary_service.Interfaces;

namespace Core.Services.Concrete.Pictures
{
    public class PicsService : IPicsService
    {

        private readonly IMapper _mapper;

        private readonly IUnitOfWork<IPicsRepo, Pics> _unitOfWork;
        private readonly ICloudinaryService _cloudinary;



        public PicsService (IMapper mapper, IUnitOfWork<IPicsRepo, Pics> uow, ICloudinaryService cloudinaryService)
        {
            _cloudinary = cloudinaryService;
            _unitOfWork = uow;
            _mapper = mapper;
        }

        public async Task<PicsDTOs.SendPicDTO?> CreatePicAsync(PicsDTOs.CreatePicDTO createPicDTO)
        {
            if (createPicDTO == null || createPicDTO.Rank < 0) return null;


            var pic = createPicDTO.pic;

            var imageUpload = await _cloudinary.UploadImageAsync(
                pic.file.OpenReadStream(), pic.FolderPath, pic.FileName);


            var entity = new Pics()
            {
                PublicID = imageUpload.PublicId,
                Rank = createPicDTO.Rank

            };

            await _unitOfWork.EntityRepo.AddItemAsync(entity);

            await _unitOfWork.CompleteAsync();

            var output = _mapper.Map<PicsDTOs.SendPicDTO>(entity);

            output.Url = _cloudinary.FetchUrl(entity.PublicID);

            return output;

        }
    }
}
