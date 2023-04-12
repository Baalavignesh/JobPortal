using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace JobPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        UserModel User = new UserModel();

        [HttpPost]
        public UserModel LogInUser(string UserName, string Password)
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=OnlineJobPortal;Integrated Security=True;Encrypt=False;");
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"SELECT * FROM USERS WHERE UserName = {UserName} AND Password = {Password}";

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        User.UserId = (int)reader["USER_ID"];
                        User.UserName = UserName;
                        User.Role = (string)reader["ROLE"];

                        return User;
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
            return null;
        }

        [HttpPost]
        public bool CreateUser(string UserName, string Password, string Role)
        {
            try
            {

                SqlConnection connection = new SqlConnection("Data Source=5CG9441HWP;Initial Catalog=OnlineJobPortal;Integrated Security=True;Encrypt=False;");
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                try
                {
                    cmd.CommandText = $"INSERT INTO USERS(USER_NAME,PASSWORD,ROLE) VALUES ('{UserName}','{Password}','{Role}');";

                    cmd.ExecuteNonQuery();

                    return true;


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
            return false;
        }



    }
}

