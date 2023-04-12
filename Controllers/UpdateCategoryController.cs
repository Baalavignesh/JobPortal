using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    public class UpdateCategoryController : Controller
    {
        IConfiguration configuration;

        public UpdateCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public CategoryModel UpdateCategory([FromBody] CategoryModel CategoryInfo)
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
                    cmd.CommandText = $"UPDATE CATEGORY SET CATEGORY_NAME = '{CategoryInfo.CategoryName}' WHERE CATEGORY_ID = {CategoryInfo.CategoryId};";
                    cmd.ExecuteNonQuery();

                    return new CategoryModel
                    {
                        CategoryId = CategoryInfo.CategoryId,
                        CategoryName = CategoryInfo.CategoryName,
                        isUpdated = true,
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new CategoryModel
                    {
                        CategoryId = CategoryInfo.CategoryId,
                        CategoryName = CategoryInfo.CategoryName,
                        isUpdated= false,
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
                    isUpdated = false,

                };
            }
        }

    }
}
