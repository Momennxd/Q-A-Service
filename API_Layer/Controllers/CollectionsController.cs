using Core.Authorization_Services.Concrete;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Collections;
using Core.DTOs.People;
using Core.Services.Concrete.Collections;
using Core.Services.Concrete.People;
using Core.Services.Concrete.Users;
using Core.Services.Interfaces;
using Data.models.People;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace API_Layer.Controllers
{

    [Route("API/Collections")]
    [ApiController]
    [Authorize]
    public class CollectionsController : Controller
    {

        private readonly ICollectionService _collectionService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public CollectionsController(ICollectionService collectionService,
            ICollectionsAuthService collectionsAuthService)
        {
            this._collectionService = collectionService;
            this._collectionsAuthService = collectionsAuthService;
        }





        [HttpPost]
        public async Task<IActionResult> AddCollection([FromBody] CollectionsDTOs.CreateQCollectionDTO createDTO)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _collectionService.CreateCollectionAsync(createDTO, userId);

            return result == 1 ? Ok() : BadRequest();

        }


        [HttpPatch]
        public async Task<IActionResult> PatchCollection
            ([FromBody] JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> patchDoc, int CollecID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserCollecOwner(CollecID, userId))
                return Unauthorized();


            return Ok(await _collectionService.PatchCollection(patchDoc, CollecID));


        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCollection(int CollecID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserCollecOwner(CollecID, userId))
                return Unauthorized();


            await _collectionService.DeleteCollectionAsync(CollecID);

            return Ok();



        }


        /// <summary>
        /// Gets all the public collections by userID.
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("Public")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPublicCollections(int UserID)
        {
            var collec = await _collectionService.GetAllCollectionsAsync(UserID, true);

            return Ok(collec);
        }


        /// <summary>
        /// Gets all the public/Private collections by the current logged in user..
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet(@"library/Visibilty")]
        public async Task<IActionResult> GetAllLoggedInUserCollections(bool Public)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var collec = await _collectionService.GetAllCollectionsAsync(userId, Public);

            return Ok(collec);
        }


        /// <summary>
        /// Gets all the public and Private collections by the current logged in user..
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        [HttpGet("library/All")]
        public async Task<IActionResult> GetAllLoggedInUserCollections()
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var collec = await _collectionService.GetAllCollectionsAsync(userId);

            return Ok(collec);
        }


        [HttpGet]
        public async Task<IActionResult> GetCollection(int CollectionID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var collec = await _collectionService.GetCollectionByIdAsync(CollectionID);

            if (collec == null)
                return NotFound();

            if (!await _collectionsAuthService.IsUserCollecOwner(CollectionID, userId) && !collec.IsPublic)
                return Unauthorized();



            return Ok(collec);
        }





    }
}
