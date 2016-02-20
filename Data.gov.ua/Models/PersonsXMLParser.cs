using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Xml;

namespace Data.gov.ua.Models
{
    public class PersonsXMLParser
    {


        public static List<Person> GetPersonsBySAX()
        {
            string approot = HostingEnvironment.MapPath("~");
            XmlReader xmlReader = XmlReader.Create(approot+"App_Data\\bills.xml");
            List<Person> People = new List<Person>();
            while (xmlReader.Read())
            {
                if (xmlReader.Name.Equals("person") && xmlReader.NodeType == XmlNodeType.Element)
                {
                    Person person = new Person();

                    int id;
                    if (Int32.TryParse(xmlReader.GetAttribute("id"), out id))
                        person.Id = id;
                    else person.Id = null;

                    for (int i = 0; i < xmlReader.Depth; i++)
                    {
                        xmlReader.Read();
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("surname"))
                        {
                            xmlReader.Read();
                            person.Surname = xmlReader.Value;
                        }
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("firstname"))
                        {
                            xmlReader.Read();
                            person.FirstName = xmlReader.Value;
                        }
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("patronymic"))
                        {
                            xmlReader.Read();
                            person.Patronymic = xmlReader.Value;
                        }

                        People.Add(person);
                    }


                }

            }

            List<Person> distinctPeople = People.Distinct().ToList();

            return distinctPeople;
        }

    }

}