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
        public Task<ResultDto> ChargeWallet(int StudentId, int ChargeCount)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDto<int?>> CreateWalletAsync(WalletDto walletModel)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
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

        public Task<ResultDto> DeleteWalletAsync(int StudentId)
        {
            throw new NotImplementedException();
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
