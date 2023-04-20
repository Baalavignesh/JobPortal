using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        IConfiguration configuration;
        List<JobsModel> searched_jobs;

        public SearchController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("{searchname}")]
        public List<JobsModel> Search(string searchname)
        {
            searched_jobs = new List<JobsModel>();

            try
            {

                Console.WriteLine(searchname);
                string conn = configuration.GetConnectionString("OnlineJobPortal");

                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = conn;
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"SELECT * FROM JOBS WHERE JOB_TITLE LIKE '%{searchname}'";

                    Console.WriteLine($"SELECT * FROM JOBS WHERE JOB_TITLE LIKE '%{searchname}'");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine((int)reader["JOB_ID"]);
                        searched_jobs.Add(
                            new JobsModel
                            {
                                JobId = (int)reader["JOB_ID"],
                                JobName = (string)reader["JOB_TITLE"],
                                JobDescribtion = (string)reader["JOB_DESCRIPTION"],
                                SubCategoryId = (int)reader["SUBCATEGORY_ID"],
                                EmployerId = (int)reader["EMPLOYER_ID"],
                                isFetched = true,
                            }
                        );

                        return searched_jobs;

                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    searched_jobs.Add(new JobsModel
                    {
                        isFetched = false,
                    });
                    return searched_jobs;

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                searched_jobs.Add(new JobsModel
                {
                    isFetched = false,
                });
                return searched_jobs;

            }
            searched_jobs.Add(new JobsModel
            {
                isFetched = false,
            });
            return searched_jobs;
        }
    }
}
