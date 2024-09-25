using Core_Layer.models.Collections;

namespace Core.DTOs.Collections
{

    public sealed class CollectionsDTOs
    {


        public class CollectionBaseDTO
        {
            public string CollectionName { get; set; }
            public string? Description { get; set; }
            public bool IsPublic { get; set; }
        }



        public class SendCollectionDTO : CollectionBaseDTO
        { 
           public int CollectionID { get; set; }
           public DateTime AddedTime { get; set; }


          //public IEnumerable<CollectionsCategories> Categories { get; set; }


        }


        public class CreateQCollectionDTO : CollectionBaseDTO
        {


        }




    }
}
