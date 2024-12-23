using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITeacherService
    {
        Task<ResultDto<int?>> AddTeacher(TeacherDto teacher);
        Task<ResultDto<TeacherDto?>> GetTeacherById(int teacherId);
        Task<ResultDto<TeacherDto?>> GetTeacherByPersonId(int personId);
        Task<ResultDto> EditTeacher(TeacherDto teacher);
        Task<ResultDto> DeleteTeacher(int teacherId);

    }
}
