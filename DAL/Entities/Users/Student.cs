
namespace DAL.Entities.Users
{
    public class Student
    {
        public int Id{ get; set; }
        public bool? Lockout {  get; set; }
        public string Major {  get; set; }
        public string? LinkedId {  get; set; }
        public string? ActivityArea {  get; set; }
        public int PersonId {  get; set; }
    }
}
