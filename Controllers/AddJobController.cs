using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddJobController : ControllerBase
    {
        IConfiguration configuration;

        public AddJobController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public JobsModel AddJob([FromBody] JobsModel JobsInfo)
        {
            Console.WriteLine("add job working");
            try
            {
                Console.WriteLine("add job working");
                Console.WriteLine(JobsInfo.JobId);
                Console.WriteLine(JobsInfo.JobName);

                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"INSERT INTO JOBS(JOB_TITLE,JOB_DESCRIPTION,SUBCATEGORY_ID,EMPLOYER_ID) VALUES ('{JobsInfo.JobName}','{JobsInfo.JobDescribtion}',{JobsInfo.SubCategoryId},{JobsInfo.EmployerId});";
                    cmd.ExecuteNonQuery();
                    return new JobsModel
                    {
                        JobId = JobsInfo.JobId,
                        JobName = JobsInfo.JobName,
                        JobDescribtion = JobsInfo.JobDescribtion,
                        SubCategoryId = JobsInfo.SubCategoryId,
                        EmployerId = JobsInfo.EmployerId,

                        isInserted = true,
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

                        isInserted = false,
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

                    isInserted = false,

                };
            }
        }
    }
}
