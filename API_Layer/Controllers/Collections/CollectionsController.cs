using Core.Authorization_Services.Interfaces;
using Core.DTOs.Collections;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API_Layer.Controllers.Collections
{

  

    //READ THIS to understand collections DTOs types----->
    /// <summary>
    /// There are two types of collections that are sent to the user
    /// 1-'FULL' this type has the full most valuable collection data like all the questions, choices, basic info,
    ///  likes and dislikes.....
    /// 2-'BASIC or THUMB' this type is just to show basic info of the the collection without exposing the 
    /// internal data like question or choices.
    /// 3-'Search' this type of used to return the most basic form of a collection to make the search faster
    /// and light in the return value, it only return the allowed model properties.
    /// </summary>


    [Route("api/collections")]
    [ApiController]
    [Authorize]
    public class CollectionsController : Controller
    {

  

        private readonly ICollectionService _collectionService;
        private readonly ICollectionsAuthService _collectionsAuthService;

        public CollectionsController(ICollectionService collectionService,
            ICollectionsAuthService collectionsAuthService)
        {
            _collectionService = collectionService;
            _collectionsAuthService = collectionsAuthService;
        }



        /// <summary>
        /// Used to search for public collections based on text that is related to the collection info like
        /// {collection name or description} it's built on the 'full text search index' in sql server
        /// </summary>
        /// <returns></returns>
        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> CollectionsSearch(string SearchText, int PageNumber, int PageSize)
        {
            return Ok(await _collectionService.CollectionsSearch
                (SearchText, PageNumber, PageSize > DEF.MAX_SEARCH_ROWS_OUTPUT ? DEF.MAX_SEARCH_ROWS_OUTPUT : PageSize));
        }



        [HttpPost]
        public async Task<IActionResult> AddCollection([FromBody] CollectionsDTOs.CreateQCollectionDTO createDTO)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return Ok(await _collectionService.CreateCollectionAsync(createDTO, userId));
        }


       
        [HttpGet("{CollecID}")]
        public async Task<IActionResult> GetFullCollection(int CollecID)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserCollectionAccess(CollecID, userId))
                return Unauthorized();

            return Ok(await _collectionService.GetFullCollectionAsync(CollecID));

        }



        [HttpGet("users/{UserID}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetThumbCollection(int UserID)
        {        
            return Ok(await _collectionService.GetThumbCollectionsAsync(UserID, true));

        }


        /// <summary>
        /// Gets all the public and Private collections by the current logged in user..
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetThumbCollection()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return Ok(await _collectionService.GetThumbCollectionsAsync(userId, null));

        }


        [HttpPatch]
        public async Task<IActionResult> PatchCollection
            ([FromBody] JsonPatchDocument<CollectionsDTOs.PatchQCollectionDTO> patchDoc, int CollecID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserCollecOwnerAsync(CollecID, userId))
                return Unauthorized();

            return Ok(await _collectionService.PatchCollection(patchDoc, CollecID));
        }




        [HttpDelete]
        public async Task<IActionResult> DeleteCollection(int CollecID)
        {

            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (!await _collectionsAuthService.IsUserCollecOwnerAsync(CollecID, userId))
                return Unauthorized();

            return Ok(await _collectionService.DeleteCollectionAsync(CollecID));
        }




    }
}
