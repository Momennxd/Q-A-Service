using System;
using System.Collections.Generic;
using Core_Layer.models.Collections;

namespace Core_Layer.models.People;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int PersonId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

}
