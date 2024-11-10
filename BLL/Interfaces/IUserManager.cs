using BLL.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IUserManager
    {
        Task<bool> RegisterStudentAsync(StudentDto student);
    }
}
