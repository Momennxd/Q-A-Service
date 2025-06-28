using Data.models.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Collections
{
    public class Collections_Reviews : IBaseEntity<Collections_Reviews>
    {
        [Key]
        public int CollectionReviewID { get; set; }

        public int CollectionID { get; set; }

        public int UserID { get; set; }

        public string? ReviewText { get; set; }

        //([ReviewValue]>=(0) AND [ReviewValue]<=(10))
        public byte ReviewValue { get; set; }

        public DateTime ReviewDate { get; set; } = DateTime.Now;

    }
}
