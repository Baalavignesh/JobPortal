using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetSpecificJobController : Controller
    {
        IConfiguration configuration;

        public GetSpecificJobController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public JobsModel GetSpecificJob([FromBody] JobsModel JobsInfo)
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
                    cmd.CommandText = $"SELECT * FROM JOBS WHERE JOB_ID = {JobsInfo.JobId}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        return new JobsModel
                        {
                            JobId = (int)reader["JOB_ID"],
                            JobName = (string)reader["JOB_TITLE"],
                            JobDescribtion = (string)reader["JOB_DESCRIPTION"],
                            SubCategoryId = (int)reader["SUBCATEGORY_ID"],
                            EmployerId = (int)reader["EMPLOYER_ID"],
                            isFetched = true,
                        };

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new JobsModel
                    {
                        isFetched = false,
                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JobsModel
                {
                    isFetched = false,
                };

            }
            return new JobsModel
            {
                isFetched = false,
            };
        }


    }
}
