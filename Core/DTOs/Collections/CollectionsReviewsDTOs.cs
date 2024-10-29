using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Collections
{
    public class CollectionsReviewsDTOs
    {
        public class MainCollectionsReviewDTO
        {
            public int CollectionID { get; set; }
            public int UserID { get; set; }
            public string? ReviewText { get; set; }
            public byte ReviewValue { get; set; }
            public DateTime ReviewDate { get; set; } = DateTime.Now;
        }

        public class UpdateCollectionsReviewsDTO
        {
            public string? ReviewText { get; set; }
            public byte ReviewValue { get; set; }
        }
    }
}
