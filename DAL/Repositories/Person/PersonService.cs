using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DAL.Entities.Base;
using BLL.ExternalApi;

namespace DAL.Repositories.Person
{
    public class PersonService : IPersonService
    {
        private readonly string _connectionString;
        private readonly IUriComposer uriComposer;
        public PersonService(string _connectionString,IUriComposer uriComposer)
        {
            this._connectionString = _connectionString;
            this.uriComposer = uriComposer;
        }
        public async Task<ResultDto<int?>> AddPerson(PersonDto person)
        {
            var existingUserResult = await GetPersonByEmail(person.Email);
            if (existingUserResult.isSuccess)
            {
                return new ResultDto<int?>()
                {
                    isSuccess = false,
                    Message = "کاربری با این ایمیل موجود است"
                };
            }
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
                    return new ResultDto<int?>()
                    {
                        Data = personId,
                        isSuccess = true,
                        Message = $"کاربر با شناسه {personId} در سیستم ثبت شد"
                    };
                }
                else
                {
                    return new ResultDto<int?>()
                    {
                        isSuccess = false,
                        Message = "خطا در ارتباط با پایگاه داده"
                    };
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return new ResultDto<int?>()
                {
                    isSuccess = false,
                    Message = $"خطایی رخ داد {ex.Message}"
                };
            }
        }

        public async Task<ResultDto> EditPerson(PersonDto Person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdatePerson", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PersonID", Person.Id);
                    command.Parameters.AddWithValue("@FirstName", Person.FirstName);
                    command.Parameters.AddWithValue("@LastName", Person.LastName);
                    command.Parameters.AddWithValue("@Email", Person.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", Person.PhoneNumber);
                    command.Parameters.AddWithValue("@Password", Person.Password);
                    command.Parameters.AddWithValue("@AvatarPath", Person.AvatarPath);
                    await connection.OpenAsync();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                        return new ResultDto()
                        {
                            isSuccess = true,
                            Message = $"ویرایش کاربر با شناسه {Person.Id} با موفقیت انجام شد"
                        };
                    return new ResultDto()
                    {
                        isSuccess = false,
                        Message = "خطا در انجام ویرایش"
                    };
                }
            }
        }

        public async Task<ResultDto<PersonDto?>> GetPersonByEmail(string EmailAddress)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var existPerson = await connection.QuerySingleOrDefaultAsync<Personn>(
                    "GetPersonByEmail",
                    new { Email = EmailAddress },
                    commandType: CommandType.StoredProcedure);
                if (existPerson != null)
                    return new ResultDto<PersonDto?>()
                    {
                        isSuccess = true,
                        Data = new PersonDto()
                        {
                            Id = existPerson.Id,
                            FirstName = existPerson.FirstName,
                            LastName = existPerson.LastName,
                            PhoneNumber = existPerson.PhoneNumber,
                            Email = existPerson.Email,
                            Password = existPerson.Password,
                            AvatarPath = uriComposer.Compose(existPerson.AvatarPath)
                        }
                    };

                else
                    return new ResultDto<PersonDto?>()
                    {
                        isSuccess = false,
                        Message = "کاربری با این ایمیل یافت نشد"
                    };

            }
        }
    }
}
