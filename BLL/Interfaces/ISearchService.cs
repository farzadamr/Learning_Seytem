using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISearchService
    {
        Task<ResultDto<PersonDto?>> GetPersonByEmailSearch(string SearchKey);
        Task<ResultDto<StudentListDto?>> GetStudentByEmailSearch(string SearchKey);
        Task<ResultDto<TeacherListDto?>> GetTeacherByEmailSearch(string SearchKey);
    }
}
