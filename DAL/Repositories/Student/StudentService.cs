using BLL.Dtos.Course;
using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Student
{
    public class StudentService : IStudentService
    {
        private readonly string _connectionString;
 
        public StudentService(string _connectionString)
        {
            this._connectionString = _connectionString;

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

        public async Task<ResultDto> DeleteStudentById(int StudentID)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("DeleteStudentById", connection))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("StudentID", StudentID);

                        await connection.OpenAsync();

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return new ResultDto
                            {
                                isSuccess = true,
                                Message = $"دانشجو با شناسه {StudentID} با موفقیت حذف شد"
                            };
                        }
                        return new ResultDto
                        {
                            isSuccess = false,
                            Message = "خطا در برقراری ارتباط با پایگاه داده"
                        };
                    }
                    catch(Exception ex)
                    {
                        return new ResultDto
                        {
                            isSuccess = false,
                            Message = $"خطایی در طول عملیات رخ داد {ex.Message}"
                        };
                    }
                    
                }
            }
        }

        public async Task<ResultDto> EditStudentById(StudentDto studentModel)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("UpdateStudent",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("Major", studentModel.Major);
                    command.Parameters.AddWithValue("ActivityArea", (object)studentModel.ActivityArea ?? DBNull.Value);
                    command.Parameters.AddWithValue("LinkedIn", (object)studentModel.LinkedId ?? DBNull.Value);
                    command.Parameters.AddWithValue("StudentID", studentModel.Id);

                    await connection.OpenAsync();

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if(rowsAffected > 0)
                    {
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = $"دانشجو با شناسه {studentModel.Id} با موفقیت ویرایش شد"
                        };
                    }
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }

        public async Task<ResultDto<StudentDto?>> GetStudentById(int studentId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var student = await connection.QuerySingleOrDefaultAsync<StudentDto>(
                    "GetStudentById",
                    new { Id = studentId },
                    commandType: CommandType.StoredProcedure
                    );
                if(student == null)
                {
                    return new ResultDto<StudentDto?>
                    {
                        isSuccess = false,
                        Message = "کاربر یافت نشد"
                    };
                }
                return new ResultDto<StudentDto?>
                {
                    Data = new StudentDto
                    {
                        Id = student.Id,
                        ActivityArea = student.ActivityArea,
                        LinkedId = student.LinkedId,
                        Lockout = student.Lockout,
                        Major = student.Major,
                        PersonId = student.PersonId
                    },
                    isSuccess = true,
                    Message = "کاربر یافت شد"
                };
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
        public async Task<List<PLP>?> GetStudentCourses(int StudentId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var courses = await connection.QueryAsync<PLP>("GetStudentCourses", new { ID = StudentId }, commandType: CommandType.StoredProcedure);
                return courses.ToList();
            }
        }
        public async Task<ResultDto> AddCourseToStudentAsync(int CourseId, int StudentId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var id = await connection.QueryAsync<int>("AddCourseToStudent", new { SID = StudentId, CID = CourseId }, commandType: CommandType.StoredProcedure);
                    if (id == null)
                        return new ResultDto
                        {
                            isSuccess = false,
                            Message = "خطا در برقراری ارتباط با پایگاه داده"
                        };
                    return new ResultDto
                    {
                        isSuccess = true,
                        Message = "دوره مورد نظر با موفقیت خریداری شد"
                    };
                }
                catch(Exception ex)
                {
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = $"خطا در اجرای پراسیجر {ex.Message}"
                    };
                }
               
                
            }
        }

	}
}
