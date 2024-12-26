﻿using BLL.Dtos.Course;
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
                            TumbnailPath = course.TumbnailPath,
                            DemoVideoPath = string.IsNullOrWhiteSpace(course.DemoVideoPath) ? (string)null : course.DemoVideoPath,
                            Time = course.Time,
                            Rate = 0,
                            Status = course.Status
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
                        Message = $"دوره با شناسه {string.Join(" ",courseId)} با موفقیت ثبت شد"
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
            using(SqlConnection connection = new SqlConnection(_connectionString))
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
            throw new Exception("");
        }
    }
}
