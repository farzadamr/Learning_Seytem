using DAL.Entities.Base;

namespace DAL.Entities.Users
{
    public class Student : Person
    {
        public bool Lockout {  get; set; }
    }
}
