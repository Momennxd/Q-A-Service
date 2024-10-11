using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.models.Collections
{
    public class CollectionsLikes : IBaseEntity<CollectionsLikes>
    {
        [Key]
        public int LikeID { get; set; }

        public int CollectionID { get; set; }

        public int LikedUserID { get; set; }

        public DateTime LikeDate { get; set; }

        public bool Like_Dislike { get; set; }
    }
}
