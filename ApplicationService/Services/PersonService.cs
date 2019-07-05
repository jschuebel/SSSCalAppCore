using System;
using System.Collections.Generic;
using System.Linq;
using SSSCalApp.Core.DomainService;
using SSSCalApp.Core.Entity;
using SSSCalApp.Core.ApplicationService;

namespace SSSCalApp.Core.ApplicationService.Services
{
    public class PersonService: IPersonService
    {
        readonly IPersonRepository _personRepo;

        public PersonService(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        public Person NewPerson(string firstName, string lastName, string address)
        {
            var cust = new Person()
            {
                Name = firstName + " " + lastName,
                Address = new Address(){ Address1 = address}
            };

            return cust;
        }

        public Person CreatePerson(Person cust)
        {
            return _personRepo.Create(cust);
        }

        public Person FindPersonById(int id)
        {
            return _personRepo.ReadyById(id);
        }

       public IEnumerable<Person> GetAllPersons()
        {
            return _personRepo.ReadAll();
        }
     public int Count()
        {
            return _personRepo.Count();
        }
    }
}