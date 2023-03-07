using E6K_NetAng_GestContact.Dal.Entities;
using E6K_NetAng_GestContact.Dal.Mappers;
using E6K_NetAng_GestContact.Dal.Repositories;
using System.Data;
using Tools.Databases;

namespace E6K_NetAng_GestContact.Dal.Services
{
    public class ContactService : IContactRepository
    {
        private readonly IDbConnection _dbConnection;

        public ContactService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IEnumerable<Contact> Get()
        {
            return _dbConnection.ExecuteReader("SELECT Id, Nom, Prenom FROM Contact;", (dr) => dr.ToContact());
        }

        public Contact? Get(int id)
        {
            return _dbConnection.ExecuteReader("SELECT Id, Nom, Prenom FROM Contact WHERE Id = @Id;", (dr) => dr.ToContact(), parameters: new { Id = id }).SingleOrDefault();
        }

        public bool Insert(Contact contact)
        {
            return _dbConnection.ExecuteNonQuery("INSERT INTO Contact (Nom, Prenom) VALUES (@Nom, @Prenom);", parameters: new { contact.Nom, contact.Prenom }) == 1;
        }

        public bool Update(Contact contact)
        {
            return _dbConnection.ExecuteNonQuery("UPDATE Contact SET Nom = @Nom, Prenom = @Prenom WHERE Id = @Id;", parameters: new { contact.Id, contact.Nom, contact.Prenom }) == 1;
        }

        public bool ChangeName(int id, string nom)
        {
            return _dbConnection.ExecuteNonQuery("UPDATE Contact SET Nom = @Nom WHERE Id = @Id;", parameters: new { id, nom }) == 1;
        }

        public bool Delete(int id)
        {
            return _dbConnection.ExecuteNonQuery("DELETE FROM Contact WHERE Id = @Id;", parameters: new { id }) == 1;
        }
    }
}
