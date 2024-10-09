using Microsoft.AspNetCore.Authentication.Cookies;

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

           

           public ICollection<string> Categories { get; set; } = new List<string>();


        }


        public class CreateQCollectionDTO : CollectionBaseDTO
        {


        }


        public class CollectionDTO
        {
            public int CollectionID { get; set; }
            public string CollectionName { get; set; }
            public List<CategoryDTO> Categories { get; set; }
        }

        public class CategoryDTO
        {
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
        }

    }
}
