using System;
using System.Collections.Generic;
using SSSCalApp.Core.Entity;

namespace SSSCalApp.Core.ApplicationService
{
    public interface IPersonService
    {
        //New Person
        Person NewPerson(string firstName,
                                string lastName,
                                string address);

        //Create //POST
        Person CreatePerson(Person cust);
        //Read //GET
        Person FindPersonById(int id);
        //Person FindPersonByIdIncludeOrders(int id);
        IEnumerable<Person> GetAllPersons();//Filter filter);
        int Count();
        //Update //PUT
        Person UpdatePerson(Person pUpdate);
        
        //Delete //DELETE
        bool DeletePerson(int id);
    }
}