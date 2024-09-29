using System;
using System.Collections.Generic;
using Data.models.Collections;
using Data.models.Base;

namespace Data.models.People;

public partial class User : IBaseEntity<User>
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int PersonId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Person Person { get; set; }
}
