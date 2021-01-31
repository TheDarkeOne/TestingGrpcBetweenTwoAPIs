using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing.Services
{
    public interface IDataService
    {
        Task CreateUsers(Users telemetry);
        IEnumerable<Users> Users();
        Users User(int id);
    }
}
