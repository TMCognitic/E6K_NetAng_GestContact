using E6K_NetAng_GestContact.Dal.Entities;
using E6K_NetAng_GestContact.Dal.Repositories;

namespace E6K_NetAng_GestContact.Domain.Services
{
    public class EFContactService : IContactRepository
    {
        private readonly ContactDbContext _context;

        public EFContactService(ContactDbContext context)
        {
            _context = context;            
        }

        public IEnumerable<Contact> Get()
        {
            return _context.Contacts;
        }

        public Contact? Get(int id)
        {
            return _context.Contacts.Find(id);
        }

        public bool Insert(Contact contact)
        {
            _context.Add(contact);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Contact contact)
        {
            Contact? entity = _context.Contacts.Find(contact.Id);

            if(entity is null)
                return false;

            entity.Nom = contact.Nom;
            entity.Prenom = contact.Prenom;

            _context.SaveChanges();
            return true;
        }
        public bool ChangeName(int id, string nom)
        {
            Contact? entity = _context.Contacts.Find(id);

            if (entity is null)
                return false;
            
            entity.Nom = nom;

            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Contact? entity = _context.Contacts.Find(id);

            if (entity is null)
                return false;

            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
