using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dtos.Utils
{
    public class ResultDto
    {
        public string? Message { get; set; }
        public bool isSuccess { get; set; }
    }
    public class ResultDto<TData>
    {
        public TData Data { get; set; }
        public string? Message { get; set; }
        public bool isSuccess { get; set; }
    }
}
