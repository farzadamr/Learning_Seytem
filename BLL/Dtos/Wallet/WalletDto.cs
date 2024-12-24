using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Wallet
{
    public class WalletDto
    {
        public int Id { get; set; }
        public int Credit { get; set; }
        public DateTime ChargeTime { get; set; }
        public int StudentId { get; set; }
    }
}
