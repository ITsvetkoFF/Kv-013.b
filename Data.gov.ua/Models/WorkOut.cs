using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.gov.ua.Models
{
    public class WorkOut
    {
        public string DocumentType;
        public DateTime date;
        public List<workOutCommittee> workOutCommittees;
    }
}