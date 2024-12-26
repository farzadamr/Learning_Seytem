using BLL.Dtos.Course;
using BLL.Dtos.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICourseService
    {
        Task<ResultDto> AddCourseAsync(CourseDto course);
    }
}
