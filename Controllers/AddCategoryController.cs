using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddCategoryController : Controller
    {
        IConfiguration configuration;

        public AddCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public CategoryModel AddCategory([FromBody] CategoryModel CategoryInfo)
        {
            try
            {
                Console.WriteLine(CategoryInfo.CategoryId);
                Console.WriteLine(CategoryInfo.CategoryName);

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"INSERT INTO CATEGORY(CATEGORY_NAME) VALUES ('{CategoryInfo.CategoryName}');";
                    cmd.ExecuteNonQuery();
                    return new CategoryModel
                    {
                        CategoryId = CategoryInfo.CategoryId,
                        CategoryName = CategoryInfo.CategoryName,
                        isInserted = true,
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new CategoryModel
                    {
                        CategoryId = CategoryInfo.CategoryId,
                        CategoryName = CategoryInfo.CategoryName,
                        isInserted = false,
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CategoryModel
                {
                    CategoryId = CategoryInfo.CategoryId,
                    CategoryName = CategoryInfo.CategoryName,
                    isInserted = false,

                };
            }
        }
    }
}
