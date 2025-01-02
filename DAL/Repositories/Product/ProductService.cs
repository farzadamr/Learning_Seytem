using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Product
{
	public class ProductService : IProductService
	{
		private readonly string _connectionString;
		public ProductService(string _connectionString)
		{
			this._connectionString = _connectionString;
		}
		public async Task<ResultDto<List<PLP>?>> GetPLPAsync(int categoryId)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				IEnumerable<PLP> coursesEnum;
				if (categoryId != 0)
				{
					coursesEnum = await connection.QueryAsync<PLP>(
					   "GetPLPByCategoryId",
					   new { categoryId = categoryId },
					   commandType: CommandType.StoredProcedure
					   );
				}
				else
				{
					coursesEnum = await connection.QueryAsync<PLP>(
						"GetPLPAll",
						commandType: CommandType.StoredProcedure
						);

				}
				if (coursesEnum == null)
					return new ResultDto<List<PLP>?>
					{
						isSuccess = false,
						Message = "اطلاعات مورد نظر یافت نشد"
					};
				var coursesList = coursesEnum.ToList();
				return new ResultDto<List<PLP>?>
				{
					Data = coursesList,
					isSuccess = true,
					Message = "اطلاعات بازیابی شد"
				};
			}
			
		}
		public async Task<ResultDto<PDP?>> GetPDPAsync(int courseId)
		{
			using(SqlConnection connection = new SqlConnection(_connectionString))
			{
				var pdp = await connection.QuerySingleOrDefaultAsync<PDP>("GetPDPList", new { ID = courseId }, commandType: CommandType.StoredProcedure);
				if (pdp == null)
					return new ResultDto<PDP?>
					{
						isSuccess = false,
						Message = "دوره مورد نظر یافت نشد"
					};
				return new ResultDto<PDP?>
				{
					Data = pdp,
					isSuccess = true,
					Message = "اطلاعات بازیابی شد"
				};
			}
		}
		public async Task<List<EpisodeSectionDto>?> GetEpisodes(int CourseId)
		{
			using(SqlConnection connection = new SqlConnection(_connectionString))
			{
				var episodes = await connection.QueryAsync<EpisodeSectionDto>("GetEpisodes", new { ID = CourseId }, commandType: CommandType.StoredProcedure);
				if (episodes == null)
					return null;
				
				return episodes.ToList();
			}
		}

        public async Task<List<PLP>?> GetCoursesByTeacherIdAsync(int TeacherId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
			{
				var courses = await connection.QueryAsync<PLP>("GetCoursesByTeacher", new { ID = TeacherId }, commandType: CommandType.StoredProcedure);
				return courses.ToList();
			}
        }
    }
}
