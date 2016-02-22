using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Data.gov.ua.Models;

namespace Data.gov.ua.Controllers
{
    public class BillsController : ApiController
    {
        // GET api/bills
        public IEnumerable<Bill> Get()
        {
            var parser = new PersonsXMLParser();
            IEnumerable<Bill> listOfBills = parser.GetBills();
            return listOfBills;
        }

        // GET api/bills
        public IEnumerable<Bill> Get(int? limit)
        {
            var parser = new PersonsXMLParser();
            int value;
            if (limit != null && int.TryParse(limit.ToString(), out value))
            {
                var listOfBills = parser.GetBills().GetRange(0, value);
                return listOfBills;
            }
            return null;
        }

        // GET api/bills/5
        public Bill Get(int id)
        {
            var parser = new PersonsXMLParser();
            return parser.GetBills()[id];
        }
    }
}
