using BLL.Dtos.Users;
using BLL.Dtos.Utils;
using BLL.ExternalApi;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ISearchService searchService;
        private readonly IUriComposer uriComposer;
        public IndexModel(ISearchService searchService,IUriComposer uriComposer)
        {
            this.searchService = searchService;
            this.uriComposer = uriComposer;
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
                        result = new ResultPageDto(true, searchPersonResult.Message);
                        personsList = searchPersonResult.Data;
                        foreach(var person in personsList)
                        {
                            person.AvatarPath = uriComposer.Compose(person.AvatarPath);
                        }
                    }
                    break;
                case 2:
                    var searchStudentResult = await searchService.GetStudentByEmailSearch(SearchKey);
                    if (searchStudentResult.isSuccess)
                    {
                        result = new ResultPageDto(true, searchStudentResult.Message);
                        studentsList = searchStudentResult.Data;
                    }
                    break;
                case 3:
                    var searchTeacherResult = await searchService.GetTeacherByEmailSearch(SearchKey);
                    if (searchTeacherResult.isSuccess)
                    {
                        result = new ResultPageDto(true, searchTeacherResult.Message);
                        teachersList = searchTeacherResult.Data;
                        foreach (var teacher in teachersList)
                        {
                            teacher.AvatarPath = uriComposer.Compose(teacher.AvatarPath);
                        }
                    }
                    break;
            }
            return Page();
        }
    }
}
