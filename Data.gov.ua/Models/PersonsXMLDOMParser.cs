using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace Data.gov.ua.Models
{
    public class PersonsXMLDOMParser
    {
        public static List<Person> GetPersonsByDOM()
        {
            XmlDocument doc = new XmlDocument();

            string approot = HostingEnvironment.MapPath("~");

            doc.Load(approot + "App_Data\\bills.xml");

            XmlNodeList nodePersons = doc.GetElementsByTagName("person");

            List<Person> persons = new List<Person>();

            foreach (XmlNode node in nodePersons)
            {
                try
                {
                    persons.Add(new Person
                    {
                        Id = int.Parse(node.Attributes[0].Value),
                        FirstName = node.ChildNodes[1].InnerText,
                        Surname = node.ChildNodes[0].InnerText,
                        Patronymic = node.ChildNodes[2].InnerText,
                    });
                }
                catch (IndexOutOfRangeException)
                {

                    continue;
                }
                catch (FormatException)
                {
                    continue;
                }
            }

            return persons;
        }
    }
}