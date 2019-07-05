using System;
using System.Collections.Generic;
using coreevent = SSSCalApp.Core.Entity;

namespace SSSCalApp.Core.ApplicationService
{
    public interface IEventService
    {
        //New Person
        coreevent.Event NewEvent(string firstName,
                                string lastName,
                                string address);

        //Create //POST
        coreevent.Event CreateEvent(coreevent.Event evt);
        //Read //GET
        coreevent.Event GetEventById(int id);
        List<coreevent.Person> GetEventByIdWithPeople(int id);
        IEnumerable<coreevent.Event> GetAllEvents();//Filter filter);
        ICollection<coreevent.Event> GetCalculatedEventsByDateRange(DateTime startDate, DateTime endDate);
        int Count();
        //Update //PUT
        //Person UpdatePerson(Person pUpdate);
        
        //Delete //DELETE
        //bool DeleteCustomer(int id);
    }
}