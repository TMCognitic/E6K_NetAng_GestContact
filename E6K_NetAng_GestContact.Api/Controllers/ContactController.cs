using E6K_NetAng_GestContact.Api.Models.DTO;
using E6K_NetAng_GestContact.Api.Models.Forms;
using E6K_NetAng_GestContact.Dal.Entities;
using E6K_NetAng_GestContact.Dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Tools.Databases;

namespace E6K_NetAng_GestContact.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly IContactRepository _contactRepository;

        public ContactController(ILogger<ContactController> logger, IContactRepository contactRepository)
        {
            _logger = logger;
            _contactRepository = contactRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogError($"{DateTime.Now} : GetAll request received....");
            return Ok(_contactRepository.Get().ToList());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Contact? contact = _contactRepository.Get(id);

            if (contact is null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ContactDto dto)
        {
            if (_contactRepository.Insert(new Contact() { Nom = dto.Nom, Prenom = dto.Prenom}))
                return NoContent();

            return BadRequest("Something wrong...");
        }

        [HttpPut("{id}")]
        [HttpPatch("{id}")]
        public IActionResult Update(int id, [FromBody] ContactDto dto)
        {
            if (_contactRepository.Update(new Contact() { Id = id, Nom = dto.Nom, Prenom = dto.Prenom }))
                return NoContent();

            return NotFound();
        }

        [HttpPatch("ChangeName/{id}")]
        public IActionResult SetName(int id, [FromBody] FullNameValue dto)
        {
            if (_contactRepository.ChangeName(id, dto.Value))
                return NoContent();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_contactRepository.Delete(id))
                return NoContent();

            return NotFound();
        }
    }
}
