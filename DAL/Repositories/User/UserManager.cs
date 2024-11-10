using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.User
{
    public class UserManager
    {
        private readonly string _connection;
        public UserManager(string _connection)
        {
            this._connection = _connection;
        }

    }
}
