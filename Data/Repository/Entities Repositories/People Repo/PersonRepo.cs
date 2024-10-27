﻿using Data.DatabaseContext;
using Data.models.People;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.People_Repo
{
    public class PersonRepo : Repository<Person>, IPersonRepo
    {

        public PersonRepo( AppDbContext context) : base(context)
        {
        }

       

    }
}
