using BLL.Dtos.Category;
using BLL.Dtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICategoryService
    {
        Task<ResultDto> AddCategoryAsync(CategoryDto category);
        Task<ResultDto<CategoryDto?>> GetCategoryAsync(int categoryId);
        Task<ResultDto> EditCategoryAsync(CategoryDto category);
        Task<ResultDto> DeleteCategoryAsync(int categoryId);
        Task<ResultDto<List<CategoryDto>?>> GetCategoryListAsync();
    }
}
