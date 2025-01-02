using BLL.Dtos.Course;

namespace Learning_System.EndPoint.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Resume { get; set; }
        public string Avatar { get; set; }
        public List<PLP> Courses { get; set; }
    }
}
