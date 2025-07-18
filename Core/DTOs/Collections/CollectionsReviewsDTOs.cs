﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Collections
{
    public class CollectionsReviewsDTOs
    {
        public class CreateCollectionsReviewDTO
        {
            public string? ReviewText { get; set; }
            public byte ReviewValue { get; set; }
            public int CollectionID { get; set; }

        }


        public class SendCollectionsReviewsDTO
        {
            public int CollectionID { get; set; }

            public int UserID { get; set; }
            public string? ReviewText { get; set; }
            public byte ReviewValue { get; set; }
            public DateTime ReviewDate { get; set; } = DateTime.Now;
        }
    }
}
