using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Category
{
    public class categoryy
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentTypeId { get; set; }
    }
}
