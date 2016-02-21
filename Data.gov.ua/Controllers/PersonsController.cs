using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.gov.ua.Models;

namespace Data.gov.ua.Controllers
{
    public class PersonsController : ApiController
    {
        // GET api/bills
        public IEnumerable<Person> Get()
        {
            IEnumerable<Person> listOfBills = PersonsXMLDOMParser.GetPersonsByDOM();
            return listOfBills;
        }

        // GET api/bills
        public IEnumerable<Person> Get(int? limit)
        {
            int value;
            if (limit != null && int.TryParse(limit.ToString(), out value))
            {
                IEnumerable<Person> listOfBills = PersonsXMLDOMParser.GetPersonsByDOM().GetRange(0, value);
                return listOfBills;
            }
            return null;
        }

        // GET api/bills/5
        public Person Get(int id)
        {
            IEnumerable<Person> listOfBills = PersonsXMLDOMParser.GetPersonsByDOM();
            return listOfBills.ElementAt(id);
        }
    }
}
