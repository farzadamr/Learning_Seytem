using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ISearchService searchService;
        public IndexModel(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public ResultPageDto result { get; set; }
        public List<PersonDto> personsList { get; set; }
        public List<StudentListDto> studentsList { get; set; }
        public List<TeacherListDto> teachersList { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostAsync(int typeList,string SearchKey)
        {
            switch (typeList)
            {
                case 1:
                    var searchPersonResult = await searchService.GetPersonByEmailSearch(SearchKey);
                    if (searchPersonResult.isSuccess)
                    {
                        personsList = searchPersonResult.Data;
                    }
                    break;
                case 2:
                    var searchStudentResult = await searchService.GetStudentByEmailSearch(SearchKey);
                    if (searchStudentResult.isSuccess)
                    {
                        studentsList = searchStudentResult.Data;
                    }
                    break;
                case 3:
                    var searchTeacherResult = await searchService.GetTeacherByEmailSearch(SearchKey);
                    if (searchTeacherResult.isSuccess)
                    {
                        teachersList = searchTeacherResult.Data;
                    }
                    break;
            }
            return Page();
        }
    }
}
