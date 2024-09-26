﻿

using Core_Layer.models.Collections;
using Core_Layer.models.People;
using Data.models._SP_;
using Microsoft.EntityFrameworkCore;

namespace Core_Layer.AppDbContext
{
    public partial class AppDbContext
    {

        public virtual DbSet<CollectionsCategories> Collections_Categories { get; set; }

        public virtual DbSet<User> Users {  get; set; }

 
        public virtual DbSet<Person> People { get; set; }


        public virtual DbSet<QCollection> QCollections { get; set; }


        public virtual DbSet<SPCollectionCetagory> SpCollectionCetagories { get; set; }

    }
}
