using Core.Authorization_Services.Concrete;
using Core.Authorization_Services.Interfaces;
using Core.DTOs.Collections;
using Core.DTOs.People;
using Core.Services.Concrete.Collections;
using Core.Services.Concrete.People;
using Core.Services.Concrete.Users;
using Core.Services.Interfaces;
using Core_Layer.models.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Claims;

namespace API_Layer.Controllers
{

    [Route("API/Collections")]
    [ApiController]
    public class CollectionsController : Controller
    {

        //private ILogger _Logger;

        private readonly ICollectionService _collectionService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public CollectionsController(CollectionService collectionService, CollectionsAuthService collectionsAuthService)
        {
            this._collectionService = collectionService;
            this._collectionsAuthService = collectionsAuthService;
        }





        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCollection([FromBody] CollectionsDTOs.CreateQCollectionDTO createDTO)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _collectionService.CreateCollectionAsync(createDTO, userId);

            return result == 1 ? Ok() : BadRequest();

        }


        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> PatchCollection
            ([FromBody]JsonPatchDocument<CollectionsDTOs.CreateQCollectionDTO> patchDoc, int CollecID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserOwner(CollecID, userId))
               return Unauthorized();


            return Ok(await _collectionService.PatchCollection(patchDoc, CollecID));

          
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteCollection(int CollecID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserOwner(CollecID, userId))
                return Unauthorized();


            await _collectionService.DeleteCollectionAsync(CollecID);

            return Ok();



        }




    }
}
