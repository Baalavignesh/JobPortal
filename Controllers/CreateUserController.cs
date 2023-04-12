using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreateUserController : Controller
    {
        [HttpPost]
        public AuthenticatedModel CreateUser([FromBody] UserModel userinfo)
        {
            try
            {
                Console.WriteLine(userinfo.Role);
                Console.WriteLine(userinfo.UserName);
                Console.WriteLine(userinfo.Password);

                SqlConnection connection = new SqlConnection("Data Source=5CG9410FHM;Initial Catalog=OnlineJobPortal;Integrated Security=True;Encrypt=False;");
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"INSERT INTO USERS(USER_NAME,PASSWORD,ROLE) VALUES ('{userinfo.UserName}','{userinfo.Password}','{userinfo.Role}');";
                    cmd.ExecuteNonQuery();
                    return new AuthenticatedModel
                    {
                        UserId = userinfo.UserId,
                        UserName = userinfo.UserName,
                        isAuthenticated = true,
                        Role = userinfo.Role

                    };
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new AuthenticatedModel
                    {
                        UserId = userinfo.UserId,
                        UserName = userinfo.UserName,
                        isAuthenticated = false,
                        Role = userinfo.Role

                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new AuthenticatedModel
                {
                    UserId = userinfo.UserId,
                    UserName = userinfo.UserName,
                    isAuthenticated = false,
                    Role = userinfo.Role

                };
            }
        }

    }
}


