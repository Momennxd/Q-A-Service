﻿using Data.Custom_Attributes;
using Data.models.Base;
using Data.models.RefreshTokens;



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

    public int Permissions { get; set; } = 0;
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
