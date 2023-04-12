using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    public class GetAllSubCategoryController : Controller
    {

        IConfiguration configuration;
        List<SubCategoryModel> subcategories;

        public GetAllSubCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public List<SubCategoryModel> GetAllSubCategory([FromBody] SubCategoryModel SubCategoryInfo)
        {
            subcategories = new List<SubCategoryModel>();
            try
            {

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"SELECT * FROM SubCategory";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        subcategories.Add(new SubCategoryModel
                        {
                            SubCategoryId = (int)reader["SubCategory_ID"],
                            SubCategoryName = (string)reader["SubCategory_NAME"],
                            CategoryId = (int)reader["Category_ID"],
                            isFetched = true,
                        }); ;

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            subcategories.Add(new SubCategoryModel
            {
                isFetched = false,
            });
            return subcategories;
        }

    }
}
