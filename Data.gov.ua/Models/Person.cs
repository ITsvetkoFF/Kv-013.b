using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data.gov.ua.Models
{
    public class Person
    {
        public int? Id { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
    }
}