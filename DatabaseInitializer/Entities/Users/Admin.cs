using DAL.Entities.Base;

namespace DAL.Entities.Users
{
    public class Admin : Person
    {
        public string Username { get; set; }
    }
}
