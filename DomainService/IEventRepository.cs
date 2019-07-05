using System;
using System.Collections.Generic;
using coreevent = SSSCalApp.Core.Entity;

namespace SSSCalApp.Core.DomainService
{
    public interface IEventRepository
    {
        //Create Data
        //No Id when enter, but Id when exits
        coreevent.Event Create(coreevent.Event evt);
        //Read Data
        coreevent.Event GetEventById(int id);
        List<coreevent.Person> GetEventByIdWithPeople(int id);
        IEnumerable<coreevent.Event> ReadAll();
        //Update Data
        coreevent.Event Update(coreevent.Event address);
        //Delete Data
        bool Delete(int id);
        int Count();
        
    }
}