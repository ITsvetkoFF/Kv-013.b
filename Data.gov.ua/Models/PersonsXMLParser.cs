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

        public List<Bill> GetBills()
        {
            string approot = HostingEnvironment.MapPath("~");
            XmlReader xmlReader = XmlReader.Create(approot + "App_Data\\bills.xml");
            List<Bill> bills = new List<Bill>();
            while (xmlReader.Read())
            {
                if (xmlReader.Name.Equals("bill") && xmlReader.NodeType == XmlNodeType.Element)
                {
                    Bill bill = new Bill();
                    bill.id = Int32.Parse(xmlReader.GetAttribute("id"));
                    xmlReader.Read();
                    xmlReader.Read();
                    ReadValueFromNode(v => bill.type = v, "type", xmlReader);
                    ReadValueFromNode(v => bill.title = v, "title", xmlReader);
                    ReadValueFromNode(v => bill.uri = v, "uri", xmlReader);
                    ReadValueFromNode(v => bill.number = v, "number", xmlReader);
                    string regDate = null;
                    ReadValueFromNode(v => regDate = v, "registrationDate", xmlReader);
                    DateTime.TryParse(regDate, out bill.registrationDate);
                    ReadValueFromNode(v => bill.registrationSession = v, "registrationSession", xmlReader);
                    ReadValueFromNode(v => bill.registrationConvocation = v, "registrationConvocation", xmlReader);
                    Agenda agenda = new Agenda();
                    if (xmlReader.Name == "agenda")
                    {
                        xmlReader.Read();
                        xmlReader.Read();
                        ReadValueFromNode(v => agenda.number = v, "number", xmlReader);
                        string date = null;
                        ReadValueFromNode(v => date = v, "date", xmlReader);
                        DateTime.TryParse(date, out agenda.date);
                        ReadValueFromNode(v => agenda.uri = v, "uri", xmlReader);
                        bill.agenda = agenda;
                        xmlReader.Read();
                        xmlReader.Read();
                    }

                    ReadValueFromNode(v => bill.redaction = v, "redaction", xmlReader);
                    ReadValueFromNode(v => bill.rubric = v, "rubric", xmlReader);
                    ReadValueFromNode(v => bill.subject = v, "subject", xmlReader);
                    bill.initiators = new List<Person>();
                    while (!(xmlReader.Name.Equals("initiators") && xmlReader.NodeType == XmlNodeType.EndElement))
                    {
                        xmlReader.Read();
                        if (xmlReader.Name.Equals("person") && xmlReader.NodeType == XmlNodeType.Element)
                            bill.initiators.Add(GetPersonsBySAX(xmlReader));
                    }
                    bill.mainExecutives = new List<Person>();
                    while (!(xmlReader.Name.Equals("mainExecutives") && xmlReader.NodeType == XmlNodeType.EndElement))
                    {
                        xmlReader.Read();
                        if (xmlReader.Name.Equals("person") && xmlReader.NodeType == XmlNodeType.Element)
                            bill.mainExecutives.Add(GetPersonsBySAX(xmlReader));
                    }
                    bill.executives = new List<Person>();
                    while (!(xmlReader.Name.Equals("executives") && xmlReader.NodeType == XmlNodeType.EndElement))
                    {
                        xmlReader.Read();
                        if (xmlReader.Name.Equals("person") && xmlReader.NodeType == XmlNodeType.Element)
                            bill.executives.Add(GetPersonsBySAX(xmlReader));
                    }
                    bill.source = new List<Document>();
                    while (!(xmlReader.Name.Equals("source") && xmlReader.NodeType == XmlNodeType.EndElement))
                    {
                        xmlReader.Read();
                        if (xmlReader.Name.Equals("document") && xmlReader.NodeType == XmlNodeType.Element)
                            bill.source.Add(GetDocument(xmlReader));
                    }
                    bill.passings = new List<Passing>();
                    while (!(xmlReader.Name.Equals("passings") && xmlReader.NodeType == XmlNodeType.EndElement))
                    {
                        xmlReader.Read();
                        if (xmlReader.Name.Equals("passing") && xmlReader.NodeType == XmlNodeType.Element)
                            bill.passings.Add(GetPassing(xmlReader));
                    }
                    xmlReader.Read();
                    xmlReader.Read();
                    bill.workOuts = new List<WorkOut>();
                    xmlReader.Read();
                    WorkOut workout = new WorkOut();
                    xmlReader.Read();
                    xmlReader.Read();
                    xmlReader.Read();

                    ReadValueFromNode(v => workout.DocumentType = v, "documentType", xmlReader);
                    string workOutDate = null;
                    ReadValueFromNode(v => workOutDate = v, "date", xmlReader);
                    DateTime.TryParse(workOutDate, out workout.date);
                    workout.workOutCommittees = new List<workOutCommittee>();

                    while (!((xmlReader.Name.Equals("workOutCommittees") && xmlReader.NodeType == XmlNodeType.EndElement) ||
                        (xmlReader.Name.Equals("bill") && xmlReader.NodeType == XmlNodeType.EndElement)))
                    {
                        xmlReader.Read();
                        if (xmlReader.Name.Equals("workOutCommittee") && xmlReader.NodeType == XmlNodeType.Element)
                            workout.workOutCommittees.Add(GetWorkOutCommittee(xmlReader));
                    }

                    bills.Add(bill);
                    while (!(xmlReader.Name.Equals("bill") && xmlReader.NodeType == XmlNodeType.EndElement))
                    {
                        xmlReader.Read();
                    }
                    xmlReader.Read();
                }

            }
            return bills;
        }

        private workOutCommittee GetWorkOutCommittee(XmlReader xmlReader)
        {
            workOutCommittee com = new workOutCommittee();
            do
            {
                xmlReader.Read();
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("title"))
                {
                    xmlReader.Read();
                    com.title = xmlReader.Value;
                }
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("datePassed"))
                {
                    xmlReader.Read();
                    DateTime.TryParse(xmlReader.Value, out com.datePassed);
                }
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("dateGot"))
                {
                    xmlReader.Read();
                    DateTime.TryParse(xmlReader.Value, out com.dateGot);
                }
            }
            while (!(xmlReader.Name == "workOutCommittee" && xmlReader.NodeType == XmlNodeType.EndElement));

            return com;
        }

        private Passing GetPassing(XmlReader xmlReader)
        {
            Passing passing = new Passing();
            do
            {
                xmlReader.Read();
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("title"))
                {
                    xmlReader.Read();
                    passing.title = xmlReader.Value;
                }
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("date"))
                {
                    xmlReader.Read();
                    DateTime.TryParse(xmlReader.Value, out passing.date);
                }
            }
            while (!(xmlReader.Name == "passing" && xmlReader.NodeType == XmlNodeType.EndElement));

            return passing;
        }

        private void ReadValueFromNode(Func<string, string> p, string v, XmlReader xmlReader)
        {
            if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals(v))
            {
                xmlReader.Read();
                p(xmlReader.Value);
                xmlReader.Read();
                xmlReader.Read();
                xmlReader.Read();
            }
        }

        public Person GetPersonsBySAX(XmlReader xmlReader)
        {

            Person person = new Person();
            do
            {
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
                }
                xmlReader.Read();
            }
            while (!(xmlReader.Name == "person" && xmlReader.NodeType == XmlNodeType.EndElement));

            return person;
        }

        public Document GetDocument(XmlReader xmlReader)
        {

            Document doc = new Document();
            do
            {
                xmlReader.Read();
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("type"))
                {
                    xmlReader.Read();
                    doc.type = xmlReader.Value;
                }
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("date"))
                {
                    xmlReader.Read();
                    DateTime.TryParse(xmlReader.Value, out doc.date);
                }
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name.Equals("uri"))
                {
                    xmlReader.Read();
                    doc.uri = xmlReader.Value;
                }
            }
            while (!(xmlReader.Name == "document" && xmlReader.NodeType == XmlNodeType.EndElement));

            return doc;
        }
        

    }

}