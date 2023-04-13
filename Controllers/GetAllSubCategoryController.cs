using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllSubCategoryController : Controller
    {

        IConfiguration configuration;
        List<SubCategoryModel> subcategories;


        public GetAllSubCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public List<SubCategoryModel> GetAllSubCategory()
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
                    cmd.CommandText = $"SELECT SUBCATEGORY_ID, SUBCATEGORY_NAME, CATEGORY_ID FROM SUBCATEGORY";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        subcategories.Add(new SubCategoryModel
                        {
                            SubCategoryId = (int)reader["SUBCATEGORY_ID"],
                            SubCategoryName = (string)reader["SUBCATEGORY_NAME"],
                            CategoryId = (int)reader["CATEGORY_ID"],
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
