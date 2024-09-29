using Data;
using Data.models.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.models.People;

public partial class Person : IBaseEntity<Person>
{
    [Key]
    public int PersonID { get; set; }

    public string FirstName { get; set; } = null!;

    public string? SecondName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public bool? Gender { get; set; }

    public byte CountryId { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string? Email { get; set; }

    public DateTime JoinedDate { get; set; }

    public string? Notes { get; set; }

    public short PreferredLanguageId { get; set; }

    public string? PassportNumber { get; set; }

    public string? NationalNumber { get; set; }

    //public string? ProfilePicUrl { get; set; }

    //public virtual Country Country { get; set; } = null!;

    //public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();

    //public virtual Language PreferredLanguage { get; set; } = null!;

    //public virtual ICollection<User> Users { get; set; } = new List<User>();
}
