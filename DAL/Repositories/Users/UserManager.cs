using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using DAL.Entities.Base;
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
            var existingUser = await connection.QuerySingleOrDefaultAsync<Person>(
                "GetPersonByEmail",
                new { Email = person.Email },
                commandType: CommandType.StoredProcedure
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

                var personId = await connection2.ExecuteScalarAsync<int>(
                    "InsertPerson",
                    new
                    {
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        PhoneNumber = person.PhoneNumber,
                        AvatarPath = person.AvatarPath,
                        Email = person.Email,
                        Password = person.Password
                    },
                    transaction,
                    commandType: CommandType.StoredProcedure
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
                        isSuccess = false,
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

        public async Task<ResultDto<LoginDto?>> LoginPersonAsync(string Email, string Password)
        {
            var connection = new SqlConnection(_connectionString);
            var userExisted = await connection.QueryFirstOrDefaultAsync<Person>(
                    "GetPersonByEmail",
                    new { Email = Email },
                    commandType: CommandType.StoredProcedure
                );
            if (userExisted == null)
            {
                return new ResultDto<LoginDto?>()
                {
                    isSuccess = false,
                    Message = "کاربری با این ایمیل وجود ندارد"
                };
            }
            if(userExisted.Password != Password)
            {
                return new ResultDto<LoginDto?>()
                {
                    isSuccess = false,
                    Message = "کلمه عبور اشتباه است"
                };
            }
            var claims = await connection.QueryAsync<Claim>(
                "GetClaimsById",
                new { Id = userExisted.Id },
                commandType: CommandType.StoredProcedure
                );
            var claimDto = claims.Select(c => new ClaimDto()
            {
                Type = c.Type,
                Value = c.Value
            }).ToList();
            return new ResultDto<LoginDto?>()
            {
                Data = new LoginDto()
                {
                    FullName = userExisted.FirstName + " " + userExisted.LastName,
                    Id = userExisted.Id,
                    claims = claimDto
                },
                isSuccess = true,
            };
        }

    }
}
