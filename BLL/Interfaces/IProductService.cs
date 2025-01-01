using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<ResultDto<List<PLP>?>> GetPLPAsync(int categoryId);
        Task<ResultDto<PDP?>> GetPDPAsync(int courseId);
    }
}
