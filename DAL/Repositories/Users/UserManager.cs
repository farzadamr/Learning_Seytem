using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos.Users;
using BLL.Interfaces;
namespace DAL.Repositories.Users
{
    public class UserManager : IUserManager
    {
        public Task<bool> RegisterStudentAsync(StudentDto student)
        {
            throw new NotImplementedException();
        }
    }
}
