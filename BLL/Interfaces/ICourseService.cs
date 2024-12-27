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
        //course
        Task<ResultDto> AddCourseAsync(CourseDto course);
        Task<ResultDto<CourseDto?>> GetCourseAsync(int courseId);
        Task<ResultDto> EditCourseAsync(CourseDto course);
        Task<ResultDto> DeleteCourseAsync(int courseId);
        Task<ResultDto<List<CourseListDto>?>> GetCourseListAsync();
        //section
        Task<ResultDto> AddSectionAsync(AddSectionDto section);
        Task<ResultDto<List<SectionDto>?>> GetSectionListAsync(int CourseId);
        Task<ResultDto> DeleteSectionAsync(int sectionId);
        Task<ResultDto> EditSectionAsync(EditSectionDto section);
        //episode
        Task<ResultDto> AddEpisodeAsync(EpisodeDto episode);
        Task<ResultDto<List<EpisodeListDto>?>> GetEpisodeListAsync(int CourseId);
        Task<ResultDto> EditEpisodeAsync(EditEpisodeDto episode);
        Task<ResultDto> DeleteEpisodeAsync(int episodeId);
        Task<ResultDto<EpisodeDto?>> GetEpisodeAsync(int episodeId);

    }
}
