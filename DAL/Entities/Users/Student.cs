using DAL.Entities.Base;

namespace DAL.Entities.Users
{
    public class Student : Personn
    {
        public bool Lockout {  get; set; }
    }
}
