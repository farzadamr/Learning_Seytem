using BLL.Dtos.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPanel.EndPoint.Pages.Users
{
    public class IndexModel : PageModel
    {


        public ResultPageDto result { get; set; }
        public void OnGet()
        {
            result = new ResultPageDto(false, "");
        }
        public async Task<IActionResult> OnPostAsync(int typeList,string SearchKey)
        {
            switch (typeList)
            {

            }
            return Page();
        }
    }
}
