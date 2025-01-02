using BLL.Dtos.Course;

namespace Learning_System.EndPoint.Models.Users
{
    public class StudentViewModel
    {
        public int stId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string LinkedId { get; set; }
        public string Avatar { get; set; }
        public int Credit { get; set; }
        public List<PLP> PLP { get; set; }
    }
}
