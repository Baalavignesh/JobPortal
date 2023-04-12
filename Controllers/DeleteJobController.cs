using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeleteJobController : Controller
    {
        IConfiguration configuration;

        public DeleteJobController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public JobsModel DeleteJob([FromBody] JobsModel JobsInfo)
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
                    cmd.CommandText = $"DELETE FROM JOBS WHERE JOB_ID = {JobsInfo.JobId}";

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        return new JobsModel
                        {
                            isDeleted = true,
                        };

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new JobsModel
                    {
                        isDeleted = false,
                    };

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JobsModel
                {
                    isDeleted = false,
                };

            }
            return new JobsModel
            {
                isDeleted = false,
            };
        }
    }
}
