using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.SearchAndGet
{
    public class SearchService : ISearchService
    {
        public Task<ResultDto<PersonDto?>> GetPersonByEmailSearch(string SearchKey)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto<StudentListDto?>> GetStudentByEmailSearch(string SearchKey)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto<TeacherListDto?>> GetTeacherByEmailSearch(string SearchKey)
        {
            throw new NotImplementedException();
        }
    }
}
