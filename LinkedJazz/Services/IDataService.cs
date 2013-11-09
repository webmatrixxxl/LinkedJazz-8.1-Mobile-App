using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseStarterProject.Services
{
    public interface IDataService
    {
        Task<IEnumerable> GetUpdates(string UserId);


        Task<IEnumerable> GetToasts(string UserId);

        Task<IEnumerable> GetUsers(string userID);

    }
}
