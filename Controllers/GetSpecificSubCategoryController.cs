using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetSpecificSubCategoryController : Controller
    {
        IConfiguration configuration;

        public GetSpecificSubCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public SubCategoryModel GetSpecificSubCategory([FromBody] SubCategoryModel SubCategoryInfo)
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
                    cmd.CommandText = $"SELECT * FROM SUBCATEGORY WHERE SUBCATEGORY_ID = {SubCategoryInfo.SubCategoryId}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        return new SubCategoryModel
                        {
                            SubCategoryId = (int)reader["SUBCATEGORY_ID"],
                            SubCategoryName = (string)reader["SUBCATEGORY_NAME"],
                            CategoryId = (int)reader["CATEGORY_ID"],
                            isFetched = true,
                        };

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new SubCategoryModel
                    {
                        isFetched = false,
                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SubCategoryModel
                {
                    isFetched = false,
                };

            }
            return new SubCategoryModel
            {
                isFetched = false,
            };
        }

    }
}
