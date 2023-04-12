namespace JobPortal.Models
{
    public class AuthenticatedModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public bool isAuthenticated { get; set; }
    }
}
