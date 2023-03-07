using E6K_NetAng_GestContact.Api.Models.DTO;
using E6K_NetAng_GestContact.Api.Models.Entities;
using E6K_NetAng_GestContact.Api.Models.Forms;
using E6K_NetAng_GestContact.Api.Models.Mappers;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using System.Data.SqlClient;
using Tools.Databases;

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
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                List<Contact> contacts = dbConnection.ExecuteReader("SELECT Id, Nom, Prenom FROM Contact;", (dr) => dr.ToContact()).ToList();
                return Ok(contacts);
            }            
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                Contact? contact = dbConnection.ExecuteReader("SELECT Id, Nom, Prenom FROM Contact WHERE Id = @Id;", (dr) => dr.ToContact(), parameters: new { Id = id }).SingleOrDefault();

                if(contact is null)
                {
                    return NotFound();
                }

                return Ok(contact);
            }            
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactDto dto)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                int rows = dbConnection.ExecuteNonQuery("INSERT INTO Contact (Nom, Prenom) VALUES (@Nom, @Prenom);", parameters: dto);

                if (rows == 1)
                    return NoContent();

                return BadRequest("Something wrong...");
            }
        }

        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] ContactDto dto)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                int rows = dbConnection.ExecuteNonQuery("UPDATE Contact SET Nom = @Nom, Prenom = @Prenom WHERE Id = @Id;", parameters: new { id, dto.Nom, dto.Prenom });

                if (rows == 1)
                    return NoContent();

                return NotFound();
            }
        }

        [HttpPatch("ChangeName/{id}")]
        public IActionResult SetName(int id, [FromBody] FullNameValue dto)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                int rows = dbConnection.ExecuteNonQuery("UPDATE Contact SET Nom = @Nom WHERE Id = @Id;", parameters: new { id, Nom = dto.Value });

                if (rows == 1)
                    return NoContent();

                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection())
            {
                dbConnection.ConnectionString = CONNECTION_STRING;

                int rows = dbConnection.ExecuteNonQuery("DELETE FROM Contact WHERE Id = @Id;", parameters: new { id });

                if (rows == 1)
                    return NoContent();

                return NotFound();
            }
        }
    }
}
