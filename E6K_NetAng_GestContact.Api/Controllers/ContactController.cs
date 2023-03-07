using E6K_NetAng_GestContact.Api.Models.DTO;
using E6K_NetAng_GestContact.Api.Models.Entities;
using E6K_NetAng_GestContact.Api.Models.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace E6K_NetAng_GestContact.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private const string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=E6K_NetAng_GestContact;Integrated Security=True";

        [HttpGet]
        public IActionResult Get()
        {
            List<Contact> contacts = new List<Contact>();

            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;                

                using (SqlCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT Id, Nom, Prenom FROM Contact;";

                    dbConnection.Open();
                    using(SqlDataReader dbReader = dbCommand.ExecuteReader())
                    {
                        while(dbReader.Read())
                        {
                            contacts.Add(new Contact { Id = (int)dbReader["Id"], Nom = (string)dbReader["Nom"], Prenom = (string)dbReader["Prenom"] });
                        }
                    }
                }
            }

            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                using (SqlCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "SELECT Id, Nom, Prenom FROM Contact WHERE Id = @Id;";
                    dbCommand.Parameters.AddWithValue("Id", id);

                    dbConnection.Open();
                    using (SqlDataReader dbReader = dbCommand.ExecuteReader())
                    {
                        if (dbReader.Read())
                        {
                            Contact contact = new Contact { Id = (int)dbReader["Id"], Nom = (string)dbReader["Nom"], Prenom = (string)dbReader["Prenom"] };
                            return Ok(contact);
                        }

                        return NotFound();
                    }
                }
            }            
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactDto dto)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                using (SqlCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "INSERT INTO Contact (Nom, Prenom) VALUES (@Nom, @Prenom);";
                    dbCommand.Parameters.AddWithValue("Nom", dto.Nom);
                    dbCommand.Parameters.AddWithValue("Prenom", dto.Prenom);

                    dbConnection.Open();
                    int rows = dbCommand.ExecuteNonQuery();

                    if (rows == 1)
                        return NoContent();

                    return BadRequest("Something wrong...");
                }
            }
        }

        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] ContactDto dto)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                using (SqlCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "UPDATE Contact SET Nom = @Nom, Prenom = @Prenom WHERE Id = @Id;";
                    dbCommand.Parameters.AddWithValue("Nom", dto.Nom);
                    dbCommand.Parameters.AddWithValue("Prenom", dto.Prenom);
                    dbCommand.Parameters.AddWithValue("id", id);

                    dbConnection.Open();
                    int rows = dbCommand.ExecuteNonQuery();

                    if (rows == 1)
                        return NoContent();

                    return NotFound();
                }
            }
        }

        [HttpPatch("ChangeName/{id}")]
        public IActionResult SetName(int id, [FromBody] FullNameValue dto)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                using (SqlCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "UPDATE Contact SET Nom = @Nom WHERE Id = @Id;";
                    dbCommand.Parameters.AddWithValue("Nom", dto.Value);
                    dbCommand.Parameters.AddWithValue("id", id);

                    dbConnection.Open();
                    int rows = dbCommand.ExecuteNonQuery();

                    if (rows == 1)
                        return NoContent();

                    return NotFound();
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                using (SqlCommand dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = "DELETE FROM Contact WHERE Id = @Id;";
                    dbCommand.Parameters.AddWithValue("id", id);

                    dbConnection.Open();
                    int rows = dbCommand.ExecuteNonQuery();

                    if (rows == 1)
                        return NoContent();

                    return NotFound();
                }
            }
        }
    }
}
