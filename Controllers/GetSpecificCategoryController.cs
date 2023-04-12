using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetSpecificCategoryController : Controller
    {

        IConfiguration configuration;

        public GetSpecificCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public CategoryModel GetSpecificCategory([FromBody] CategoryModel CategoryInfo)
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
                    cmd.CommandText = $"SELECT * FROM CATEGORY WHERE CATEGORY_ID = {CategoryInfo.CategoryId}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                       return new CategoryModel
                        {
                            CategoryId = (int)reader["CATEGORY_ID"],
                            CategoryName = (string)reader["CATEGORY_NAME"],
                            isFetched = true,
                        }; 

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new CategoryModel
                    {
                        isFetched = false,
                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new CategoryModel
                {
                    isFetched = false,
                };

            }
            return new CategoryModel
            {
                isFetched = false,
            };
        }

    }
}
