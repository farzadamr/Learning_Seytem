namespace DAL.Entities.Courses
{
	public class Category
	{
		public long Id { set; get; }
		public long CategoryId { set; get; }
		public string Title { get; set; }
		public long ParentTypeId { get; set; }
	}
}
