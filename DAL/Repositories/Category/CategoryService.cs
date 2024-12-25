using BLL.Dtos.Category;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using DAL.Entities.Category;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Category;
using System.Data;

namespace DAL.Repositories.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly string _connectionString;
        public CategoryService(string _connectionString)
        {
            this._connectionString = _connectionString;
        }
        public async Task<ResultDto> AddCategoryAsync(CategoryDto category)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var categoryId = await connection.ExecuteScalarAsync<int>(
                    "InsertCategory",
                    new { 
                        Type = category.Type,
                        ParentTypeId = category.ParentTypeId == 0 ? (int?)null : category.ParentTypeId},
                    commandType: CommandType.StoredProcedure
                    );
                if (categoryId == null)
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در افزودن دسته بندی جدید"
                    };
                return new ResultDto
                {
                    isSuccess = false,
                    Message = $"دسته بندی جدید با شناسه {category} ثبت شد"
                };
            }
        }

        public Task<ResultDto> DeleteCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ResultDto> EditCategoryAsync(CategoryDto category)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultDto<CategoryDto?>> GetCategoryAsync(int categoryId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var Findcategory = await connection.QuerySingleOrDefaultAsync<categoryy>(
                        "GetCategory",
                        new { Id = categoryId },
                        commandType: CommandType.StoredProcedure
                        );
                    if (Findcategory == null)
                        return new ResultDto<CategoryDto?>
                        {
                            isSuccess = false,
                            Message = "دسته بندی یافت نشد"
                        };

                    return new ResultDto<CategoryDto?>
                    {
                        Data = new CategoryDto
                        {
                            Id = Findcategory.Id,
                            Type = Findcategory.Type,
                            ParentTypeId = Findcategory.ParentTypeId
                        },
                        isSuccess = true,
                        Message = "دسته بندی یافت شد"
                    };
                }
                catch(Exception ex)
                {
                    return new ResultDto<CategoryDto?>
                    {
                        isSuccess = false,
                        Message = $"خطایی در طول عملیات رخ داد {ex.Message}"
                    };
                }
            }
        }
        public async Task<ResultDto<List<CategoryDto>?>> GetCategoryListAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var categoryEnumerable = await connection.QueryAsync<CategoryDto>("GetCategoryList", commandType: CommandType.StoredProcedure);
                var categories = categoryEnumerable.AsList();

                return new ResultDto<List<CategoryDto>?>
                {
                    Data = categories,
                    isSuccess = true
                };
            }
        }

    }
}
