using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteSubCategoryController : Controller
    {
        IConfiguration configuration;

        public DeleteSubCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public SubCategoryModel DeleteSubCategory([FromBody] SubCategoryModel SubCategoryInfo)
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
                    cmd.CommandText = $"DELETE FROM SubCategory WHERE SubCategory_ID = {SubCategoryInfo.SubCategoryId}";

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        return new SubCategoryModel
                        {
                            isDeleted = true,
                        };

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new SubCategoryModel
                    {
                        isDeleted = false,
                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SubCategoryModel
                {
                    isDeleted = false,
                };

            }
            return new SubCategoryModel
            {
                isDeleted = false,
            };
        }

    }
}
