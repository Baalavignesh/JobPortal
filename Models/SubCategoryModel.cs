namespace JobPortal.Models
{
    public class SubCategoryModel
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }

        public bool isInserted { get; set; }
        public bool isFetched { get; set; }
        public bool isUpdated { get; set; }
        public bool isDeleted { get; set; }
    }
}
