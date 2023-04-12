using Microsoft.AspNetCore.Mvc;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteSubCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
