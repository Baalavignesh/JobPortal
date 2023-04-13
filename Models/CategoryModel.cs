namespace JobPortal.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public bool isInserted{ get; set; }
        public bool isFetched { get; set; }
        public bool isUpdated{ get; set; }
        public bool isDeleted { get; set; }
    }
}
