using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Authorization_Services.Interfaces
{
    public interface ICollectionsAuthService
    {


        /// <summary>
        /// checks if the user is the collection creater.
        /// </summary>
        /// <param name="collecID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public Task<bool> IsUserOwner(int collecID, int UserID);
       















    }
}
