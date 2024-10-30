using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Data.models.Base;
using Microsoft.EntityFrameworkCore;

namespace Data.models.Collections;

public partial class QCollection : IBaseEntity<QCollection>
{

    [Key]
    public int CollectionId { get; set; }

    public string CollectionName { get; set; }

    public string? Description { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime AddedTime { get; set; }


    public bool IsPublic { get; set; }

}
