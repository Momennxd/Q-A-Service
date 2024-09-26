using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Core;
using Core_Layer.models.People;
using Microsoft.EntityFrameworkCore;

namespace Core_Layer.models.Collections;

public partial class QCollection : IBaseEntity
{

    [Key]
    public int CollectionId { get; set; }

    public string CollectionName { get; set; }

    public string? Description { get; set; }

    public int CreatedByUserId { get; set; }

    public DateTime AddedTime { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsPublic { get; set; }


}
