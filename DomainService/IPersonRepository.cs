using System;
using System.Collections.Generic;
using SSSCalApp.Core.Entity;

namespace SSSCalApp.Core.DomainService
{
    public interface IPersonRepository
    {
        //Create Data
        //No Id when enter, but Id when exits
        Person Create(Person person);
        //Read Data
        Person GetById(int id);
        IEnumerable<Person> ReadAll();
        //Update Data
        Person Update(Person person);
        //Delete Data
        bool Delete(int id);
        int Count();
        
    }
}