using Data.DatabaseContext;
using Data.models.Pictures;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository.Entities_Repositories.Pics.Choices_Pics_Repo
{
    public class ChoicesPicsRepo : Repository<ChoicesPics>, IChoicesPicsRepo
    {
        private AppDbContext _appDbContext;
        public ChoicesPicsRepo(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }






    }
}
