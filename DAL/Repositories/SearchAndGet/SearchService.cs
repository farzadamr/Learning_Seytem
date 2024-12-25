using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.SearchAndGet
{
    public class SearchService : ISearchService
    {
        private readonly string _connectionString;
        public SearchService(string _connectionString)
        {
            this._connectionString = _connectionString;
        }
        public async Task<ResultDto<List<PersonDto>?>> GetPersonByEmailSearch(string SearchKey)
        {
            IEnumerable<PersonDto> personsEnum = new List<PersonDto>();
            if (string.IsNullOrWhiteSpace(SearchKey)) 
            {
                using(SqlConnection connection = new SqlConnection(_connectionString))
                {
                    personsEnum = await connection.QueryAsync<PersonDto>(
                        "GetPersonList",
                        commandType: CommandType.StoredProcedure);
                }
            }
            else
            {
                using (SqlConnection connection2 = new SqlConnection(_connectionString))
                {
                    personsEnum = await connection2.QueryAsync<PersonDto>(
                            "SearchPersonList",
                            new { SeachKey = SearchKey },
                            commandType: CommandType.StoredProcedure);
                }
            }
            var persons = personsEnum.AsList();
            if (persons == null) return new ResultDto<List<PersonDto>?> { isSuccess = false, Message = "خطا در ارتباط با پایگاه داده" };
            return new ResultDto<List<PersonDto>?>
            {
                Data = persons,
                isSuccess = true,
                Message = "اطلاعات بازیابی شد"
            };
        }

        public async Task<ResultDto<List<StudentListDto>?>> GetStudentByEmailSearch(string SearchKey)
        {
            IEnumerable<StudentListDto> studentsEnum = new List<StudentListDto>();
            if (string.IsNullOrWhiteSpace(SearchKey))
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    studentsEnum = await connection.QueryAsync<StudentListDto>(
                        "GetStudentList",
                        commandType: CommandType.StoredProcedure);
                }
            }
            else
            {
                using (SqlConnection connection2 = new SqlConnection(_connectionString))
                {
                    studentsEnum = await connection2.QueryAsync<StudentListDto>(
                            "SearchStudentList",
                            new { SearchKey = SearchKey },
                            commandType: CommandType.StoredProcedure);
                }
            }
            var students = studentsEnum.AsList();
            if (students == null) return new ResultDto<List<StudentListDto>?> { isSuccess = false, Message = "خطا در ارتباط با پایگاه داده" };
            return new ResultDto<List<StudentListDto>?>
            {
                Data = students,
                isSuccess = true,
                Message = "اطلاعات بازیابی شد"
            };
        }

        public async Task<ResultDto<List<TeacherListDto>?>> GetTeacherByEmailSearch(string SearchKey)
        {
            IEnumerable<TeacherListDto> teachersEnum = new List<TeacherListDto>();
            if (string.IsNullOrWhiteSpace(SearchKey))
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    teachersEnum = await connection.QueryAsync<TeacherListDto>(
                        "GetTeacherList",
                        commandType: CommandType.StoredProcedure);
                }
            }
            else
            {
                using (SqlConnection connection2 = new SqlConnection(_connectionString))
                {
                    teachersEnum = await connection2.QueryAsync<TeacherListDto>(
                            "SearchTeacherList",
                            new { SearchKey = SearchKey },
                            commandType: CommandType.StoredProcedure);
                }
            }
            var teachers = teachersEnum.AsList();
            if (teachers == null) return new ResultDto<List<TeacherListDto>?> { isSuccess = false, Message = "خطا در ارتباط با پایگاه داده" };
            return new ResultDto<List<TeacherListDto>?>
            {
                Data = teachers,
                isSuccess = true,
                Message = "اطلاعات بازیابی شد"
            };
        }
    }
}
