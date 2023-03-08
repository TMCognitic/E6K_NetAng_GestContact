using E6K_NetAng_GestContact.Dal.Entities;
using E6K_NetAng_GestContact.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E6K_NetAng_GestContact.Dal.Services
{
    public class FakeContactService : IContactRepository
    {
        private static List<Contact>? _items;

        public FakeContactService()
        {
            if (_items is null)
            {
                _items = new List<Contact>()
                {
                    new Contact() { Id = 1, Nom = "Doe", Prenom = "John" },
                    new Contact() { Id = 2, Nom = "Doe", Prenom = "Jane" }
                };
            }
        }

        public bool ChangeName(int id, string nom)
        {
            Contact? c = _items!.Where(c => c.Id == id).SingleOrDefault();

            if (c is null)
                return false;

            return true;
        }

        public bool Delete(int id)
        {
            Contact? c = _items!.Where(c => c.Id == id).SingleOrDefault();

            if (c is null)
                return false;

            _items!.Remove(c);
            return true;
        }

        public IEnumerable<Contact> Get()
        {
            return _items!;
        }

        public Contact? Get(int id)
        {
            return _items!.Where(c => c.Id == id).SingleOrDefault();
        }

        public bool Insert(Contact contact)
        {
            contact.Id = (_items!.Count == 0) ? 1 : _items.Max(c => c.Id) + 1;
            _items.Add(contact);
            return true;
        }

        public bool Update(Contact contact)
        {
            Contact? c = _items!.Where(c => c.Id == contact.Id).SingleOrDefault();

            if(c is null)
                return false;

            return true;
        }
    }
}
