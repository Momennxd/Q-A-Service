using Core_Layer.models.Collections;

namespace Core.DTOs.Collections
{

    public class QCollectionsDTOs
    {
        public record SendQCollectionDTO
        (
           int CollectionID,
           string CollectionName,
           string? Description,
           DateTime AddedTime,
           bool IsPublic,
           IEnumerable<CollectionsCategories> Categories


        );


        public record CreateQCollectionDTO
        (
          string CollectionName,
          string? Description,
          bool IsPublic
        );




    }
}
