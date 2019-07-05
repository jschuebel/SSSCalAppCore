using System;
using System.Collections.Generic;
using SSSCalApp.Core.Entity;

namespace SSSCalApp.Core.DomainService
{
    public interface IAddressRepository
    {
        //Create Data
        //No Id when enter, but Id when exits
        Address Create(Address address);
        //Read Data
        Address ReadyById(int id);
        IEnumerable<Address> ReadAll();
        //Update Data
        Address Update(Address address);
        //Delete Data
        bool Delete(int id);
        int Count();
        
    }
}