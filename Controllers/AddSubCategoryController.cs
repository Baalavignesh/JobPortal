using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddSubCategoryController : Controller
    {
        IConfiguration configuration;

        public AddSubCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public SubCategoryModel AddSubCategory([FromBody] SubCategoryModel SubCategoryInfo)
        {
            try
            {
                Console.WriteLine(SubCategoryInfo.SubCategoryId);
                Console.WriteLine(SubCategoryInfo.SubCategoryName);
                Console.WriteLine(SubCategoryInfo.CategoryId);

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"INSERT INTO SUBCATEGORY(SUBCATEGORY_NAME,CATEGORY_ID) VALUES ('{SubCategoryInfo.SubCategoryName}',{SubCategoryInfo.CategoryId});";
                    cmd.ExecuteNonQuery();
                    return new SubCategoryModel
                    {
                        SubCategoryId = SubCategoryInfo.SubCategoryId,
                        SubCategoryName = SubCategoryInfo.SubCategoryName,
                        CategoryId = SubCategoryInfo.CategoryId,
                        isInserted = true,
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new SubCategoryModel
                    {
                        SubCategoryId = SubCategoryInfo.SubCategoryId,
                        SubCategoryName = SubCategoryInfo.SubCategoryName,
                        CategoryId = SubCategoryInfo.CategoryId,
                        isInserted = false,
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new SubCategoryModel
                {
                    SubCategoryId = SubCategoryInfo.SubCategoryId,
                    SubCategoryName = SubCategoryInfo.SubCategoryName,
                    CategoryId = SubCategoryInfo.CategoryId,
                    isInserted = false,

                };
            }
        }
    }
}
