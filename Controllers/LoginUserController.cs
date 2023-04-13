using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginUserController : Controller
    {
        IConfiguration configuration;

        public LoginUserController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        public AuthenticatedModel LoginUser([FromBody] UserModel userinfo)
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
                    Console.WriteLine(userinfo.UserName);
                    Console.WriteLine(userinfo.Password);
                    cmd.CommandText = $"SELECT * FROM USERS WHERE USER_NAME = '{userinfo.UserName}' AND PASSWORD = '{userinfo.Password}'";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine("DB data");
                        Console.WriteLine((int)reader["USER_ID"]);
                        Console.WriteLine((string)reader["ROLE"]);
                        return new AuthenticatedModel
                        {
                            UserId = (int)reader["USER_ID"],
                            UserName = (string)reader["USER_NAME"],
                            isAuthenticated = true,
                            Role = (string)reader["ROLE"]
                        };
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new AuthenticatedModel
                    {
                        isAuthenticated = false,
                    };
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuthenticatedModel
                {
                    isAuthenticated = false,
                };
            }
            return new AuthenticatedModel
            {
                isAuthenticated = false,
            };
        }
    }
}


