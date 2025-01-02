using BLL.Dtos.Course;

namespace Learning_System.EndPoint.Models.Product
{
    public class PDPViewModel
    {
        public PDP pdpData { get; set; }
        public List<EpisodeSectionDto> episodes { get; set; }
    }
}
