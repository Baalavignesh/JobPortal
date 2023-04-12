using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
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
                    cmd.CommandText = $"SELECT * FROM SubCategory WHERE SubCategory_ID = {SubCategoryInfo.SubCategoryId}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        return new SubCategoryModel
                        {
                            SubCategoryId = (int)reader["SubCategory_ID"],
                            SubCategoryName = (string)reader["SubCategory_NAME"],
                            CategoryId = (int)reader["Category_ID"],
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
