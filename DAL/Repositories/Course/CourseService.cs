using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using DAL.Entities.Courses;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Course
{
    public class CourseService : ICourseService
    {
        private readonly string _connectionString;
        public CourseService(string _connectionString)
        {
            this._connectionString = _connectionString;
        }
        //course services
        public async Task<ResultDto> AddCourseAsync(CourseDto course)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    var courseId = await connection.QueryAsync<int>(
                        "InsertCourse",
                        new
                        {
                            TeacherId = course.TeacherId,
                            CategoryId = course.CategoryId,
                            Title = course.Title,
                            Description = course.Description,
                            CreationDate = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            Capacity = course.Capacity,
                            TumbnailPath = course.ThumbnailPath,
                            DemoVideoPath = string.IsNullOrWhiteSpace(course.DemoVideoPath) ? (string)null : course.DemoVideoPath,
                            Time = course.Time,
                            Rate = 0,
                            Status = course.Status,
                            Price = course.Price
                        },
                        commandType: CommandType.StoredProcedure
                        );
                    if (courseId == null)
                        return new ResultDto
                        {
                            isSuccess = false,
                            Message = "خطا در برقراری ارتباط با پایگاه داده"
                        };
                    return new ResultDto
                    {
                        isSuccess = true,
                        Message = $"دوره با شناسه {string.Join(" ", courseId)} با موفقیت ثبت شد"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultDto
                {
                    isSuccess = false,
                    Message = $"خطایی در طول عملیات رخ داد {ex.Message}"
                };
            }


        }

        public async Task<ResultDto<CourseDto?>> GetCourseAsync(int courseId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var existcourse = await connection.QuerySingleOrDefaultAsync<CourseDto>(
                    "GetCourse",
                    new
                    {
                        Id = courseId
                    },
                    commandType: CommandType.StoredProcedure
                    );
                if (existcourse == null)
                    return new ResultDto<CourseDto?>
                    {
                        isSuccess = false,
                        Message = "دوره مورد نظر یافت نشد"
                    };
                return new ResultDto<CourseDto?>
                {
                    Data = existcourse,
                    isSuccess = true,
                    Message = "دوره مورد نظر بازیابی شد"
                };

            }
        }

        public async Task<ResultDto> EditCourseAsync(CourseDto course)
        {
            var existCourse = await GetCourseAsync(course.Id);
            if (!existCourse.isSuccess)
            {
                return new ResultDto
                {
                    isSuccess = false,
                    Message = existCourse.Message,
                };
            }
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {

                using (SqlCommand command = new SqlCommand("EditCourse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", course.Id);
                    command.Parameters.AddWithValue("Title", course.Title);
                    command.Parameters.AddWithValue("Description", course.Description);
                    command.Parameters.AddWithValue("CreationDate", course.CreationDate);
                    command.Parameters.AddWithValue("UpdateTime", DateTime.Now);
                    command.Parameters.AddWithValue("Capacity", course.Capacity);
                    command.Parameters.AddWithValue("ThumbnailPath", string.IsNullOrWhiteSpace(course.ThumbnailPath) ? existCourse.Data.ThumbnailPath : course.ThumbnailPath);
                    command.Parameters.AddWithValue("DemoVideoPath", string.IsNullOrWhiteSpace(course.DemoVideoPath) ? (string)null : course.DemoVideoPath);
                    command.Parameters.AddWithValue("Time", course.Time);
                    command.Parameters.AddWithValue("Status", course.Status);
                    command.Parameters.AddWithValue("Price", course.Price);

                    await connection.OpenAsync();
                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = $"ویرایش دوره {string.Join(" ", course.Id)} با موفقیت انجام شد"
                        };
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }

        public async Task<ResultDto> DeleteCourseAsync(int courseId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteCourse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", courseId);

                    await connection.OpenAsync();

                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = $"دوره با شناسه {string.Join("", courseId)} با موفقیت حذف شد"
                        };
                    return new ResultDto()
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }

        public async Task<ResultDto<List<CourseListDto>?>> GetCourseListAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var coursesListEnum = await connection.QueryAsync<CourseListDto>(
                    "GetCourseList",
                    commandType: CommandType.StoredProcedure
                    );
                var coursesList = coursesListEnum.ToList();
                if (coursesList == null)
                    return new ResultDto<List<CourseListDto>?>
                    {
                        isSuccess = false,
                        Message = "خطا در بازیابی اطلاعات"
                    };
                return new ResultDto<List<CourseListDto>?>
                {
                    Data = coursesList,
                    isSuccess = true,
                    Message = "اطلاعات بازیابی شد"
                };
            }
        }
        //section services

        public async Task<ResultDto> AddSectionAsync(AddSectionDto section)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var InsertedId = await connection.QueryAsync<int>("AddSection", new
                {
                    CourseId = section.courseId,
                    Number = section.number,
                    Title = section.Title
                },
                commandType: CommandType.StoredProcedure);
                if (InsertedId != null)
                {
                    return new ResultDto
                    {
                        isSuccess = true,
                        Message = $"فصل با شناسه {string.Join("", InsertedId)} با موفقیت ثبت شد"
                    };
                }
                return new ResultDto
                {
                    isSuccess = false,
                    Message = "خطا در برقراری ارتباط با پایگاه داده"
                };
            }
        }

        public async Task<ResultDto<List<SectionDto>?>> GetSectionListAsync(int CourseId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    var sectionsEnum = await connection.QueryAsync<SectionDto>("GetSectionList", new { CourseId }, commandType: CommandType.StoredProcedure);
                    var sectionList = sectionsEnum.ToList();
                    if (sectionList == null)
                        return new ResultDto<List<SectionDto>?>
                        {
                            isSuccess = false,
                            Message = "اطلاعاتی موجود نیست"
                        };
                    return new ResultDto<List<SectionDto>?>
                    {
                        Data = sectionList,
                        isSuccess = true,
                        Message = "اطلاعات بازیابی شد"
                    };
                }
                catch(Exception ex)
                {
                    return new ResultDto<List<SectionDto>?>
                    {
                        isSuccess = false,
                        Message = $"خطایی رخ داد {string.Join("=", ex.Message)}"
                    };
                }
                
            }

        }

        public async Task<ResultDto> DeleteSectionAsync(int sectionId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("DeleteSection", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", sectionId);

                    await connection.OpenAsync();

                    int rows = await command.ExecuteNonQueryAsync();

                    if (rows > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = $"فصل {sectionId} با موفقیت حذف شذ"
                        };
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }
    
        public async Task<ResultDto> EditSectionAsync(EditSectionDto section)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("EditSection", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", section.Id);
                    command.Parameters.AddWithValue("Title", section.Title);
                    command.Parameters.AddWithValue("Number", section.number);

                    await connection.OpenAsync();

                    int rows = await command.ExecuteNonQueryAsync();

                    if (rows > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = $"فصل {section.Id} با موفقیت ویرایش شد"
                        };
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }
        //episode services
        public async Task<ResultDto> AddEpisodeAsync(EpisodeDto episode)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var InsertedId = await connection.QueryAsync<int>(
                    "AddEpisode",
                    new
                    {
                        number = episode.number,
                        sectionId = episode.sectionId,
                        courseId = episode.CourseId,
                        time = episode.Time,
                        visit = episode.Visit,
                        filePath = episode.FilePath
                    },
                    commandType: CommandType.StoredProcedure
                    );
                if (InsertedId == null)
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                return new ResultDto
                {
                    isSuccess = true,
                    Message = $"قسمت جدید با شماره {InsertedId} با موفقیت ثبت شد"
                };
            }
        }

        public async Task<ResultDto<List<EpisodeDto>?>> GetEpisodeListAsync(int CourseId, int SectionId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                var EpisodesEnum = await connection.QueryAsync<EpisodeDto>(
                    "GetEpisodeList",
                    new
                    {
                        courseId = CourseId,
                        sectionId = SectionId
                    },
                    commandType: CommandType.StoredProcedure
                    );
                var Episodes = EpisodesEnum.ToList();
                if (Episodes == null)
                    return new ResultDto<List<EpisodeDto>?>
                    {
                        isSuccess = false,
                        Message = "اطلاعات مورد نظر یافت نشد"
                    };
                return new ResultDto<List<EpisodeDto>?>
                {
                    Data = Episodes,
                    isSuccess = true,
                    Message = "اطلاعات بازیابی شد"
                };
            }
        }

        public async Task<ResultDto> EditEpisodeAsync(EditEpisodeDto episode)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("EditEpisode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", episode.Id);
                    command.Parameters.AddWithValue("number", episode.number);
                    command.Parameters.AddWithValue("time", episode.Time);
                    command.Parameters.AddWithValue("filePath", episode.FilePath);

                    await connection.OpenAsync();

                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = $"قسمت شماره {episode.number} با موفقیت ویرایش شد"
                        };
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }

        public async Task<ResultDto> DeleteEpisodeAsync(int episodeId)
        {
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                using(SqlCommand command = new SqlCommand("DeleteEpisode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Id", episodeId);

                    await connection.OpenAsync();

                    int rows = await command.ExecuteNonQueryAsync();
                    if (rows > 0)
                        return new ResultDto
                        {
                            isSuccess = true,
                            Message = "حذف قسمت با موفقیت انجام شد"
                        };
                    return new ResultDto
                    {
                        isSuccess = false,
                        Message = "خطا در برقراری ارتباط با پایگاه داده"
                    };
                }
            }
        }
    }
}
