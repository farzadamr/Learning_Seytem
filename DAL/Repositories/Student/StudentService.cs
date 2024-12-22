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

namespace DAL.Repositories.Student
{
    public class StudentService : IStudentService
    {
        private readonly string _connectionString;
        private readonly IPersonService personService;
        public StudentService(string _connectionString, IPersonService personService)
        {
            this._connectionString = _connectionString;
            this.personService = personService;
        }
        public async Task<ResultDto<int?>> AddStudent(StudentDto student)
        {
            var existingStudent = await GetStudentByPersonId(student.PersonId);
            if (existingStudent.isSuccess)
            {
                return new ResultDto<int?>
                {
                    isSuccess = false,
                    Message = "دانشجو با این مشخصات وجود دارد"
                };
            }
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var studentId = await connection.ExecuteScalarAsync<int>(
                    "AddStudent",
                    new
                    {
                        PersonID = student.PersonId,
                        Lockout = student.Lockout,
                        Major = student.Major,
                        LinkedId = student.LinkedId,
                        ActivityArea = student.ActivityArea
                    },
                    commandType: CommandType.StoredProcedure
                    );
                    if (studentId == null)
                    {
                        return new ResultDto<int?>
                        {
                            isSuccess = false,
                            Message = "خطا در برقراری ارتباط با پایگاه داده"
                        };
                    }
                    return new ResultDto<int?>
                    {
                        Data = studentId,
                        isSuccess = true,
                        Message = $"دانشجو با شناسه {studentId} ثبت شد"
                    };
                }
                catch(Exception ex)
                {
                    return new ResultDto<int?>
                    {
                        isSuccess = false,
                        Message = $"خطایی در طول عملیات رخ داد {ex.Message}"
                    };
                }
                
            }
        }

        public async Task<ResultDto<StudentDto?>> GetStudentByPersonId(int PersonID)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var existStudent = await connection.QuerySingleOrDefaultAsync<StudentDto>(
                    "GetStudentByPersonID",
                    new { PersonID = PersonID },
                    commandType: CommandType.StoredProcedure
                    );
                if(existStudent == null)
                {
                    return new ResultDto<StudentDto?>
                    {
                        isSuccess = false,
                        Message = "دانشجویی با این مشخصات وجود ندارد",
                    };
                }
                return new ResultDto<StudentDto?>
                {
                    Data = new StudentDto()
                    {
                        Id = existStudent.Id,
                        Major = existStudent.Major,
                        LinkedId = existStudent.LinkedId,
                        ActivityArea = existStudent.ActivityArea,
                        Lockout = existStudent.Lockout,
                        PersonId = existStudent.PersonId
                    },
                    isSuccess = true,
                    Message = "کاربر یافت شد"
                };
            }
        }
    }
}
