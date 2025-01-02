using BLL.Dtos.Course;
using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStudentService
    {
        Task<ResultDto<int?>> AddStudent(StudentDto student);
        Task<ResultDto<StudentDto?>> GetStudentByPersonId(int PersonID);
        Task<ResultDto<StudentDto?>> GetStudentById(int studentId);
        Task<ResultDto> EditStudentById(StudentDto studentModel);
        Task<ResultDto> DeleteStudentById(int StudentID);
        Task<List<PLP>?> GetStudentCourses(int StudentId);
        Task<ResultDto> AddCourseToStudentAsync(int CourseId,int StudentId);
    }
}
