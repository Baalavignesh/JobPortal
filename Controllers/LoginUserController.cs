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
                    cmd.CommandText = $"SELECT * FROM USERS WHERE UserName = {userinfo.UserName}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        if (userinfo.Password == reader["PASSWORD"])
                        {
                            return new AuthenticatedModel
                            {
                                UserId = userinfo.UserId,
                                UserName = userinfo.UserName,
                                isAuthenticated = true,
                                Role = (string)reader["ROLE"]
                            };
                        }
                        else
                        {
                            return new AuthenticatedModel
                            {
                                isAuthenticated = false,
                            };
                        }
                        
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


