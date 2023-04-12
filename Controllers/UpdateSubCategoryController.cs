using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateSubCategoryController : Controller
    {
        IConfiguration configuration;

        public UpdateSubCategoryController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public SubCategoryModel UpdateSubCategory([FromBody] SubCategoryModel SubCategoryInfo)
        {
            try
            {
                Console.WriteLine(SubCategoryInfo.SubCategoryId);
                Console.WriteLine(SubCategoryInfo.SubCategoryName);

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"UPDATE SUBCATEGORY SET SUBCATEGORY_NAME = '{SubCategoryInfo.SubCategoryName}', CATEGORY_ID = {SubCategoryInfo.CategoryId} WHERE SUBCATEGORY_ID = {SubCategoryInfo.SubCategoryId};";
                    cmd.ExecuteNonQuery();

                    return new SubCategoryModel
                    {
                        SubCategoryId = SubCategoryInfo.SubCategoryId,
                        SubCategoryName = SubCategoryInfo.SubCategoryName,
                        CategoryId = SubCategoryInfo.CategoryId,
                        isUpdated = true,
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
                        isUpdated = false,
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
                    isUpdated = false,

                };
            }
        }
    }
}
