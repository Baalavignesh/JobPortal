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
                    cmd.CommandText = $"SELECT * FROM USERS WHERE USER_NAME = {userinfo.UserName}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Console.WriteLine("DB data");
                        Console.WriteLine(reader.GetString(0));
                        Console.WriteLine(reader.GetString(1));
                        Console.WriteLine(reader.GetString(2));
                        if (userinfo.Password == reader.GetString(3))
                        {
                            return new AuthenticatedModel
                            {
                                UserId = reader.GetInt32(0),
                                UserName = reader.GetString(1),
                                isAuthenticated = true,
                                Role = reader.GetString(2)
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


