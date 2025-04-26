using System;
using System.Collections.Generic;
using Data.models.Collections;
using Data.models.Base;
using System.ComponentModel.DataAnnotations;
using Data.Custom_Attributes;




namespace Data.models.People;

public partial class User : IBaseEntity<User>
{
    public int UserId { get; set; }

    [MinStringLength(5, ErrorMessage = "Username must be at least 5 characters long.")]
    public required string Username { get; set; }

    [MinStringLength(8, ErrorMessage = "password must be at least 8 characters long.")]
    public required string Password { get; set; }

    public int PersonId { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public Person Person { get; set; }

    public byte Permissions { get; set; } = 0;
}
