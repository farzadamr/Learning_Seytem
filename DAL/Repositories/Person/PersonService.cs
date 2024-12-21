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

namespace DAL.Repositories.Person
{
	public class PersonService : IPersonService
	{
		private readonly string _connectionString;
		public PersonService(string _connectionString)
		{
			this._connectionString = _connectionString;
		}
		public async Task<ResultDto<int?>> AddPerson(PersonDto person)
		{
			using var connection = new SqlConnection(_connectionString);
			var existingUser = await connection.QuerySingleOrDefaultAsync<Personn>(
				"GetPersonByEmail",
				new { Email = person.Email },
				commandType: CommandType.StoredProcedure
			);
			if (existingUser != null)
			{
				return new ResultDto<int?>()
				{
					isSuccess = false,
					Message = "کاربری با این ایمیل موجود است"
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
            throw new NotImplementedException();
        }

        public async Task<ResultDto<PersonDto?>> GetPersonByEmail(string EmailAddress)
        {
            throw new NotImplementedException();
        }
    }
}
