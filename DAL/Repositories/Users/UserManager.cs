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
        public async Task<ResultDto> RegisterPersonAsync(PersonDto person)
        {
            using var connection = new SqlConnection(_connectionString);
            var existingUser = await connection.QuerySingleOrDefaultAsync<Student>(
                "SELECT * FROM Person WHERE Email = @Email",
                new { Email = person.Email }
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
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        PhoneNumber = person.PhoneNumber,
                        AvatarPath = person.AvatarPath,
                        Email = person.Email,
                        Password = person.Password
                    },
                    transaction
                    );
                transaction.Commit();
                if (personId != null)
                {
                    return new ResultDto()
                    {
                        isSuccess = true,
                        Message = $"ثبت کاربر با شماره {personId} با موفقیت انجام شد"
                    };
                }
                else
                {
                    transaction.Rollback();
                    return new ResultDto()
                    {
                        isSuccess =false,
                        Message = $"خطا در ارتباط با دیتابیس"
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
