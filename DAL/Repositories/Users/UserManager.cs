using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using DAL.Entities.Users;
using Dapper;
namespace DAL.Repositories.Users
{
    public class UserManager : IUserManager
    {
        private readonly string _connectionString;
        public UserManager(string _connectionString)
        {
            this._connectionString = _connectionString;

        }

        public async Task<ResultDto> RegisterStudentAsync(StudentDto student)
        {
            using var connection = new SqlConnection(_connectionString);
            var existingUser = await connection.QuerySingleOrDefaultAsync<Student>(
                "SELECT * FROM Person WHERE Email = @Email",
                new { Email = student.Email }
                );
            if (existingUser != null)
            {
                return new ResultDto()
                {
                    Message = "کاربری با این ایمیل وجود دارد",
                    isSuccess = false
                };
            }
            await connection.CloseAsync();

            using var connection2 = new SqlConnection(_connectionString); 
            await connection2.OpenAsync();
            using var transaction = connection2.BeginTransaction(); 
            try
            { // Insert into Person table
                var personSql = "INSERT INTO Person (FirstName, LastName, PhoneNumber,AvatarPath,Email,Password) VALUES (@FirstName, @LastName, @PhoneNumber,@AvatarPath,@Email,@Password);" +
                    "SELECT CAST(SCOPE_IDENTITY() as int)";
                int personId = await connection2.ExecuteScalarAsync<int>
                    (personSql,
                    new
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        PhoneNumber = student.PhoneNumber,
                        AvatarPath = student.AvatarPath,
                        Email = student.Email,
                        Password = student.Password
                    },
                    transaction
                    );
                // Insert into Student table with the newly created PersonId
                var studentSql = "INSERT INTO Student (PersonId, Lockout) VALUES (@PersonId, @Lockout)";
                var rowsAffected = await connection2.ExecuteAsync(studentSql,
                    new
                    {
                        PersonId = personId,
                        Lockout = false
                    },
                    transaction);
                transaction.Commit();
                if (rowsAffected > 0)
                {
                    return new ResultDto()
                    {
                        isSuccess = true,
                        Message = $"ثبت کاربر با شماره {personId} با موفقیت انجام شد"
                    };
                }
                else
                {
                    return new ResultDto()
                    {
                        isSuccess =false,
                        Message = $"خطای مقدار {rowsAffected}"
                    };
                }
            }
            catch
            {
                transaction.Rollback();
                return new ResultDto()
                {
                    isSuccess = false,
                    Message = "خطایی در طول عملیات رخ داد"
                };
            }
        }

        public Task<ResultDto<LoginStudentDto>> LoginStudentAsync(string Email, string Password)
        {
            throw new NotImplementedException();
        }
    }
}
