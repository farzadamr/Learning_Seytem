using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using DAL.Entities.Users;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Users;
using System.Net.Http.Headers;

namespace DAL.Repositories.Teacher
{
    public class TeacherService : ITeacherService
    {
        private readonly string _connectionString;
        public TeacherService(string _connectionString)
        {
            this._connectionString = _connectionString;
        }
        public async Task<ResultDto<int?>> AddTeacher(TeacherDto teacher)
        {
            var existingTeacher = await GetTeacherByPersonId(teacher.PersonID);
            if (existingTeacher.isSuccess)
            {
                return new ResultDto<int?>
                {
                    isSuccess = false,
                    Message = "مدرس با این مشخصات در سیستم موجود است"
                };
            }
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                int TeacherID = await connection.ExecuteScalarAsync<int>(
                    "InsertTeacher",
                    new { Resume = teacher.Resume, PersonID = teacher.PersonID },
                    commandType: CommandType.StoredProcedure);
                if(TeacherID == null)
                {
                    return new ResultDto<int?>
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
                return new ResultDto<int?>
                {
                    isSuccess = true,
                    Message = $"مدرس با شناسه {TeacherID} با موفقیت ثبت شد"
                };

            }
        }
        public async Task<ResultDto<TeacherDto?>> GetTeacherById(int teacherId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var teacher = await connection.QuerySingleOrDefaultAsync<Teacherr>(
                    "GetTeacherById",
                    new { Id = teacherId },
                    commandType: CommandType.StoredProcedure);
                    
                if(teacher == null)
                {
                    return new ResultDto<TeacherDto?>
                    {
                        isSuccess = false,
                        Message = "مدرس با این مشخصات وجود ندارد"
                    };
                }
                return new ResultDto<TeacherDto?>
                {
                    isSuccess = true,
                    Message = "کاربر یافت شد",
                    Data = new TeacherDto
                    {
                        Id = teacher.Id,
                        Resume = teacher.Resume,
                        PersonID = teacher.PersonId
                    }
                };
            }
        }

        public async Task<ResultDto<TeacherDto?>> GetTeacherByPersonId(int personId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var findteacher = await connection.QuerySingleOrDefaultAsync<Teacherr>(
                    "GetTeacherByPersonId",
                    new { PersonId = personId },
                    commandType: CommandType.StoredProcedure
                    );
                if(findteacher == null)
                {
                    return new ResultDto<TeacherDto?>
                    {
                        isSuccess = false,
                        Message = "کاربر یافت نشد"
                    };
                }
                return new ResultDto<TeacherDto?>
                {
                    Data = new TeacherDto
                    {
                        Id = findteacher.Id,
                        PersonID = findteacher.PersonId,
                        Resume = findteacher.Resume
                    },
                    isSuccess = true,
                    Message = "کاربر یافت شد"
                };
            }
        }
    }
}
