using BLL.Dtos.Utils;
using BLL.Dtos.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IWalletService
    {
        Task<ResultDto<int?>> CreateWalletAsync(int StudentId);
        Task<ResultDto<WalletDto?>> GetWalletAsync(int StudentId);
        Task<ResultDto> ChargeWallet(int StudentId, int ChargeCount); 
        Task<ResultDto> DeleteWalletAsync(int StudentId);
    }
}
