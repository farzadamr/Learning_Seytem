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
                            new { SearchKey = SearchKey },
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

        Task<ResultDto<List<StudentListDto>?>> ISearchService.GetStudentByEmailSearch(string SearchKey)
        {
            throw new NotImplementedException();
        }

        Task<ResultDto<List<TeacherListDto>?>> ISearchService.GetTeacherByEmailSearch(string SearchKey)
        {
            throw new NotImplementedException();
        }
    }
}
