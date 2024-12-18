using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Users
{
    public class ClaimData
    {
            public int Id { get; set; }
            public int PersonId { get; set; }
            public string Type { get; set; }
            public string Value { get; set; }
            public DateTime CreationDate { get; set; }

    }
}
