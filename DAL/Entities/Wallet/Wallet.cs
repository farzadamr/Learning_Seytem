using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Wallet
{
    public class Wallet
    {
        public int Id { get; set; }
        public int Credit { get; set; }
        public DateTime ChargeTime { get; set; }
        public int StudentId { get; set; }
    }
}
