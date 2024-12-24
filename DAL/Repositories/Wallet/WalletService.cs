using BLL.Dtos.Utils;
using BLL.Dtos.Wallet;
using BLL.Interfaces;
using DAL.Entities.Wallet;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Wallet
{
    public class WalletService : IWalletService
    {
        private readonly string _connectionString;
        public WalletService(string _connectionString)
        {
            this._connectionString = _connectionString;
        }
        public async Task<ResultDto> ChargeWallet(int StudentId, int ChargeCount)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("UpdateWallet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("StudentID", StudentId);
                    command.Parameters.AddWithValue("Credit", ChargeCount);
                    command.Parameters.AddWithValue("ChargeTime", DateTime.Now);

                    await connection.OpenAsync();

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    if (rowsAffected > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = "شارژ کیف پول با موفقیت انجام شد"
                        };
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
                
            }
        }

        public async Task<ResultDto<int?>> CreateWalletAsync(WalletDto walletModel)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var existingWallet = await GetWalletAsync(walletModel.StudentId);
                if (existingWallet.isSuccess)
                    return new ResultDto<int?>
                    {
                        isSuccess = false,
                        Message = "کیف پول برای این کاربر موجود است. از گزینه شارژ استفاده کنید"
                    };
                try
                {
                    var walletId = await connection.ExecuteScalarAsync<int>(
                    "InsertWallet",
                    new { StudentID = walletModel.StudentId, Credit = walletModel.Credit, ChargeTime = DateTime.Now },
                    commandType: CommandType.StoredProcedure
                    );
                    if (walletId == null || walletId == 0)
                        return new ResultDto<int?>
                        {
                            isSuccess = false,
                            Message = "خطا در ارتباط با پایگاه داده"
                        };
                    return new ResultDto<int?>
                    {
                        Data = walletId,
                        isSuccess = true,
                        Message = $"کیف پول با شناسه {walletId} برای کاربر {walletModel.StudentId} با موفقیت ثبت شد"
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

        public async Task<ResultDto> DeleteWalletAsync(int StudentId)
        {
            using(SqlConnection  connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("DeleteWallet",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("StudentID", StudentId);

                    await connection.OpenAsync();

                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    if (rowsAffected > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = "کیف پول با موفقیت حذف شد"
                        };
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "حطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }

        public async Task<ResultDto<WalletDto?>> GetWalletAsync(int StudentId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var wallet = await connection.QuerySingleOrDefaultAsync<wallet>(
                    "GetWallet",
                    new { StudentID = StudentId },
                    commandType: CommandType.StoredProcedure
                    );
                if (wallet == null)
                    return new ResultDto<WalletDto?>
                    {
                        isSuccess = false,
                        Message = "کیف پول برای این دانشجو یافت نشد"
                    };
                return new ResultDto<WalletDto?>
                {
                    isSuccess = true,
                    Message = "کیف پول یافت شد",
                    Data = new WalletDto
                    {
                        Id = wallet.Id,
                        Credit = wallet.Credit,
                        ChargeTime = wallet.ChargeTime,
                        StudentId = wallet.StudentId
                    }
                };

            }
        }
    }
}
