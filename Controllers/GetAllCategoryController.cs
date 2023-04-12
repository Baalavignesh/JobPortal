using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    public class GetAllCategoryController : Controller
    {
        IConfiguration configuration;
        List<CategoryModel> categories;

        public GetAllCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public List<CategoryModel> GetAllCategory([FromBody] CategoryModel CategoryInfo)
        {
            categories = new List<CategoryModel>();
            try
            {

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"SELECT * FROM CATEGORY";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        categories.Add(new CategoryModel
                        {
                            CategoryId = (int)reader["CATEGORY_ID"],
                            CategoryName = (string)reader["CATEGORY_NAME"],
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
            categories.Add(new CategoryModel
            {
                isFetched = false,
            });
            return categories;
        }
    }
}
