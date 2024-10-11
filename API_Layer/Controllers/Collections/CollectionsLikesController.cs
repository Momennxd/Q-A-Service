using API_Layer.Security;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Layer.Controllers.Collections
{
    [Route("api/CollectionsLikes")]
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
        public async Task<IActionResult> LikeDislike(int CollectionId, bool IsLike)
        {
            int UserId = clsToken.GetUserID(HttpContext);
            await _collectionService.LikeAsync(UserId, CollectionId, IsLike);
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int CollectionId)
        {
            int UserId = clsToken.GetUserID(HttpContext);
            await _collectionService.DeleteLikeAsync(CollectionId, UserId);
            return Ok();
        }
    }
}
