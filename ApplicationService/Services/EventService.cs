using System;
using System.Collections.Generic;
using System.Linq;
using SSSCalApp.Core.DomainService;
using coreevent = SSSCalApp.Core.Entity;
using SSSCalApp.Core.ApplicationService;

namespace SSSCalApp.Core.ApplicationService.Services
{
    public class EventService: IEventService
    {
        readonly IEventRepository _eventRepo;

        public EventService(IEventRepository eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public coreevent.Event NewEvent(string firstName, string lastName, string address)
        {
            var evt = new coreevent.Event()
            {
                //Name = firstName + " " + lastName,
                //Address = new Address(){ Address1 = address}
            };

            return evt;
        }

        public coreevent.Event CreateEvent(coreevent.Event evt)
        {
            return _eventRepo.Create(evt);
        }
       public coreevent.Event UpdateEvent(coreevent.Event evt)
        {
            return _eventRepo.Update(evt);
        }
       public bool DeleteEvent(int id)
        {
            return _eventRepo.Delete(id);
        }


        public coreevent.Event GetEventById(int id)
        {
            return _eventRepo.GetEventById(id);
        }
        public  List<coreevent.Person> GetEventByIdWithPeople(int id)
        {
            return _eventRepo.GetEventByIdWithPeople(id);
        }

        public DateTime GetEaster(int wYear) {
            double g, c , h , i , j , p;
            int wDay , wMonth;
            //test  3/31/2013  var wYear
            //                     wYear=2013

            g= Math.Floor((double)wYear % 19);
            c= Math.Floor((double)wYear / 100);
            h= Math.Floor((c - (c/4) - ((8*c+13) / 25) + (19 * g) + 15) % 30);
            i= Math.Floor(h - (h/28) * (1-(h/28) * (29/h+1) * ((21-g)/11)));
            j= Math.Floor((wYear + (wYear/4) + i + 2 - c + (c/4)) % 7);
            p= Math.Floor(i - j + 28);
            wDay = (int)p;
            wMonth = 4;
            if (p > 31.0)
                wDay = (int)Math.Round(p - 31);
            else
                wMonth = 3;
            return new DateTime(wYear, wMonth, wDay);
            //alert("Easter Dt=" + est);
        }
        public  ICollection<coreevent.Event> GetCalculatedEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            var lstEvents = new List<coreevent.Event>();

            var startMonth=startDate.Month;
            var endMonth = endDate.Month;

            var hldStDate = new DateTime(startDate.Year, startDate.Month, 1);
            var currYear = endDate.Year;
            var mnths = new System.Text.StringBuilder();
            DateTime estr, ashWednesday;

            while(hldStDate<=endDate) {
                if (mnths.Length>0) mnths.Append(",");
                mnths.Append(hldStDate.Month);
                currYear = hldStDate.Year;


                estr=GetEaster(hldStDate.Year) ;
                if ((hldStDate.Month==2 || hldStDate.Month== 3 || hldStDate.Month == 4) && hldStDate.Month==estr.Month) {
                //console.log("*********** Easter="+ estr);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Holy Day",
                        topicf = new coreevent.Topic() { TopicTitle="Holy Day" },
                        Description = "Easter",
                        Date = estr
                        });
                }

                ashWednesday= estr.AddDays(-46);
                if (hldStDate.Month==ashWednesday.Month){
                    //console.log("*********** ashWednesday="+ ashWednesday);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Holy Day" ,
                        topicf = new coreevent.Topic() { TopicTitle="Holy Day" },
                        Description = "Ash Wednesday",
                        Date = ashWednesday
                        });
                }


                //feb Presidents day 3rd Monday of Febuary; 0=Sunday  evtid=231
                if (hldStDate.Month==1) {
                    var dtm = new DateTime(hldStDate.Year, 2,1);
                    int dofw = (int)dtm.DayOfWeek;
                    int daystilSun=8-(int)dtm.DayOfWeek;
                    //if already at Sunday
                    if (dofw==0)
                        daystilSun=1; //add 1 for monday
                    dtm=dtm.AddDays(daystilSun+14);          
                    //console.log("*********** presday="+ dtm);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Observed Holiday" ,
                        topicf = new coreevent.Topic() { TopicTitle="Observed Holiday" },
                        Description = "Presidents Day",
                        Date = dtm
                        });
                }

                //Mar spring forward 2nd Sunday of March   evtid=234
                if (hldStDate.Month==2) {
                    var dtm = new DateTime(hldStDate.Year,3,1);
                    int dofw = (int)dtm.DayOfWeek;
                    int daystilSun=7-(int)dtm.DayOfWeek;
                    //if already at Sunday
                    if (dofw==0)
                        daystilSun=0;
                    dtm = dtm.AddDays(daystilSun+7);            
                    //console.log("*********** Daylight Saving Time Spring Forward="+ dtm);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Clock Change",
                        topicf = new coreevent.Topic() { TopicTitle="Clock Change" },
                        Description = "Daylight Saving Time Spring Forward",
                        Date = dtm
                        });
                }	

                //May
                if (hldStDate.Month==4) {
                    //Mothers day 2nd Sunday of May   evtid=236
                    var dtm = new DateTime(hldStDate.Year,5,1);
                    int dofw = (int)dtm.DayOfWeek;
                    int daystilSun=7-(int)dtm.DayOfWeek;
                    //if already at Sunday
                    if (dofw==0)
                        daystilSun=0;
                    dtm = dtm.AddDays(daystilSun+7);

                    //console.log("*********** Mothers Day="+ dtm);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Special Day" ,
                        topicf = new coreevent.Topic() { TopicTitle="Special Day" },
                        Description = "Mothers Day",
                        Date = dtm
                        });

                    //Memorial day Last Monday of May   evtid=237
                    var dtm2 = new DateTime(hldStDate.Year,5,31);
                    dofw = (int)dtm2.DayOfWeek;
                    if (dofw>1)
                        dtm2 = dtm2.AddDays(-(dofw-1));
                    else {
                        if (dofw==0)
                            dtm2 = dtm2.AddDays(-(dofw+6));
                    }
                    //console.log("*********** Memorial Day="+ dtm2);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Observed Holiday" ,
                        topicf = new coreevent.Topic() { TopicTitle="Observed Holiday" },
                        Description = "Memorial Day",
                        Date = dtm2
                        });
                }

                //Jun Fathers day 3rd Sunday of June   evtid=239
                if (hldStDate.Month==5) {
                    var dtm = new DateTime(hldStDate.Year,6,1);
                    int dofw = (int)dtm.DayOfWeek;
                    int daystilSun=7-(int)dtm.DayOfWeek;
                    //if already at Sunday
                    if (dofw==0)
                        daystilSun=0;
                    dtm = dtm.AddDays(daystilSun+14);
                    //console.log("*********** Father's Day="+ dtm);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Special Day" ,
                        topicf = new coreevent.Topic() { TopicTitle="Special Day" },
                        Description = "Father's Day",
                        Date = dtm
                        });
                }

                //Sept Labor Day 1st Monday   evtid=241
                if (hldStDate.Month==8) {
                    var dtm = new DateTime(hldStDate.Year,9,1);
                    int dofw = (int)dtm.DayOfWeek;
                    int daystilSun=8-(int)dtm.DayOfWeek;
                    //if already at Sunday
                    if (dofw==0)
                        daystilSun=1;
                    if (dofw==1)
                        daystilSun=0;
                    dtm = dtm.AddDays(daystilSun);
                    //console.log("*********** Labor Day="+ dtm);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Observed Holiday",
                        topicf = new coreevent.Topic() { TopicTitle="Observed Holiday" },
                        Description = "Labor Day",
                        Date = dtm
                        });
                }

                //Oct Columbus Day 2nd Monday   evtid=242
                if (hldStDate.Month==9) {
                    var dtm = new DateTime(hldStDate.Year,10,1);
                    int dofw = (int)dtm.DayOfWeek;
                    int daystilSun=8-(int)dtm.DayOfWeek;
                    //if already at Sunday
                    if (dofw==0)
                        daystilSun=1;
                    if (dofw==1)
                        daystilSun=0;
                    dtm = dtm.AddDays(daystilSun+7);
                    //console.log("*********** Columbus Day="+ dtm);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Observed Holiday",
                        topicf = new coreevent.Topic() { TopicTitle="Observed Holiday" },
                        Description = "Columbus Day",
                        Date = dtm
                        });
                }

                //Nov
                if (hldStDate.Month==10) {
                    //Fall Back 1st Sunday  evtid=234
                    var dtm = new DateTime(hldStDate.Year,11,1);
                    int dofw = (int)dtm.DayOfWeek;
                    int daystilSun=7-(int)dtm.DayOfWeek;
                    //if already at Sunday
                    if (dofw==0)
                        daystilSun=0;
                    dtm = dtm.AddDays(daystilSun);
                    //console.log("*********** Daylight Savings Fall Back="+ dtm);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Clock Change" ,
                        topicf = new coreevent.Topic() { TopicTitle="Clock Change" },
                        Description = "Daylight Savings Fall Back",
                        Date = dtm
                        });

                    //ThanksGiving 4th Thursday of November   evtid=129
                    var dtm2 = new DateTime(hldStDate.Year,11,30);
                    dofw = (int)dtm2.DayOfWeek;
                    if (dofw>5)
                        dtm2 = dtm2.AddDays(-(dofw-4));
                    else
                        dtm2 = dtm2.AddDays(-(dofw+3));
                    //console.log("*********** ThanksGiving="+ dtm2);
                    lstEvents.Add(new coreevent.Event {
                        topic = "Observed Holiday" ,
                        topicf = new coreevent.Topic() { TopicTitle="Observed Holiday" },
                        Description = "ThanksGiving",
                        Date = dtm2
                        });
                }

                hldStDate = hldStDate.AddMonths(1);

            }
            return lstEvents;
        }

       public IEnumerable<coreevent.Event> GetAllEvents()
        {
            return _eventRepo.ReadAll();
        }
     public int Count()
        {
            return _eventRepo.Count();
        }
    }
}