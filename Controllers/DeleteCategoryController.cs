using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteCategoryController : Controller
    {
        IConfiguration configuration;

        public DeleteCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public CategoryModel DeleteCategory([FromBody] CategoryModel CategoryInfo)
        {

            try
            {
                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"DELETE FROM Category WHERE Category_ID = {CategoryInfo.CategoryId}";

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        return new CategoryModel
                        {
                            isDeleted = true,
                        };

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new CategoryModel
                    {
                        isDeleted = false,
                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CategoryModel
                {
                    isDeleted = false,
                };

            }
            return new CategoryModel
            {
                isDeleted = false,
            };
        }

    }
}
