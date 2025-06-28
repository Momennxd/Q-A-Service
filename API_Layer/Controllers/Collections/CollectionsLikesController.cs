using API_Layer.Extensions;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Layer.Controllers.Collections
{
    //this controller is tested and works fine by momen on 28th june 2025
    [Route("api/v1/collections/likes")]
    [ApiController]
    public class CollectionsLikesController : ControllerBase
    {
        private readonly ICollectionService _collectionService;

        public CollectionsLikesController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPost("Like")]
        [Authorize]
        //tested and works fine by moment on 28th june 2025
        public async Task<IActionResult> LikeDislike(int CollectionId, bool IsLike)
        {
            int? userId =User.GetUserId();
            if (userId == null) return Unauthorized();

            bool Res = await _collectionService.LikeAsync((int)userId, CollectionId, IsLike);
            return Res ? Ok() : BadRequest();
        }


        ///this endpoint is commented out because it is not used in the current implementation
        //[HttpDelete]
        //[Authorize]
        //public async Task<IActionResult> Delete(int CollectionId)
        //{
        //    int? userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        //    if (userId == null) return Unauthorized();

        //    bool Res = await _collectionService.DeleteLikeAsync(CollectionId, (int)userId);
        //    return Res ? Ok() : BadRequest();
        //}
    }
}
