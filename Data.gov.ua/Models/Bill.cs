using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.gov.ua.Models
{
    public class Bill
    {
        public int id;
        public string type;
        public string title;
        public string uri;
        public string number;
        public DateTime registrationDate;
        public string registrationSession;
        public string registrationConvocation;
        public Agenda agenda;
        public string redaction;
        public string rubric;
        public string subject;
        public List<Person> initiators;
        public List<Person> mainExecutives;
        public List<Person> executives;
        public List<Document> source;
        //public List<Document> workflow;
        public List<Passing> passings;
        public List<WorkOut> workOuts;
        //public int refBillId;
        //public List<int> AlternativeRefBillsId;
    }
}