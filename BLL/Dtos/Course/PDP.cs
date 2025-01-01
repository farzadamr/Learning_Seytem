namespace BLL.Dtos.Course
{
    public class PDP
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Time { get; set; }
        public string Title { get; set; }
        public int Rate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Type { get; set; }
        public string ThumbnailPath { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }
        public string PersonAvatar { get; set; }
    }
}
