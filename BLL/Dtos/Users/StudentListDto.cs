namespace BLL.Dtos.Users
{
    public class StudentListDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LinkedId { get; set; }
        public string ActivityArea { get; set; }
        public string Major { get; set; }
        public bool Lockout { get; set; }
        public int PersonId { get; set; }
    }
}
