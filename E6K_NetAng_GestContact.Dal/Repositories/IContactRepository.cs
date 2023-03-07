using E6K_NetAng_GestContact.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E6K_NetAng_GestContact.Dal.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> Get();
        Contact? Get(int id);
        bool Insert(Contact contact);
        bool Update(Contact contact);
        bool ChangeName(int id, string nom);
        bool Delete(int id);
    }
}
