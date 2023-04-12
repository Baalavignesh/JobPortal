using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UpdateJobController : Controller
    {
        IConfiguration configuration;

        public UpdateJobController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public JobsModel UpdateJob([FromBody] JobsModel JobsInfo)
        {
            try
            {
                Console.WriteLine(JobsInfo.JobId);
                Console.WriteLine(JobsInfo.JobName);

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"UPDATE JOBS SET JOB_TITLE = '{JobsInfo.JobName}', JOB_DESCRIPTION = '{JobsInfo.JobDescribtion}', SUBCATEGORY_ID = {JobsInfo.SubCategoryId}, EMPLOYER_ID = {JobsInfo.EmployerId} WHERE JOB_ID = {JobsInfo.JobId};";
                    cmd.ExecuteNonQuery();

                    return new JobsModel
                    {
                        JobId = JobsInfo.JobId,
                        JobName = JobsInfo.JobName,
                        JobDescribtion = JobsInfo.JobDescribtion,
                        SubCategoryId = JobsInfo.SubCategoryId,
                        EmployerId = JobsInfo.EmployerId,

                        isUpdated = true,
                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new JobsModel
                    {
                        JobId = JobsInfo.JobId,
                        JobName = JobsInfo.JobName,
                        JobDescribtion = JobsInfo.JobDescribtion,
                        SubCategoryId = JobsInfo.SubCategoryId,
                        EmployerId = JobsInfo.EmployerId,

                        isUpdated = false,
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new JobsModel
                {
                    JobId = JobsInfo.JobId,
                    JobName = JobsInfo.JobName,
                    JobDescribtion = JobsInfo.JobDescribtion,
                    SubCategoryId = JobsInfo.SubCategoryId,
                    EmployerId = JobsInfo.EmployerId,

                    isUpdated = false,

                };
            }
        }
    }
}
