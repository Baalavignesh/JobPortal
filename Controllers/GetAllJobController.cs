using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetAllJobController : Controller
    {
        IConfiguration configuration;
        List<JobsModel> jobs;

        public GetAllJobController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public List<JobsModel> GetAllJob()
        {
            jobs = new List<JobsModel>();
            try
            {

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = "SELECT * FROM JOBS";

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine((int)reader["JOB_ID"]);
                        jobs.Add(new JobsModel
                        {
                            JobId = (int)reader["JOB_ID"],
                            JobName = (string)reader["JOB_TITLE"],
                            JobDescribtion = (string)reader["JOB_DESCRIPTION"],
                            SubCategoryId = (int)reader["SUBCATEGORY_ID"],
                            EmployerId = (int)reader["EMPLIYER_ID"],
                            isFetched = true,
                        });

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
            return jobs;
        }

    }
}
