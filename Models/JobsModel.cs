namespace JobPortal.Models
{
    public class JobsModel
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public string JobDescribtion { get; set; }
        public int SubCategoryId { get; set; }
        public int EmployerId { get; set; }
    }
}
