using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Xml;

namespace Student_App
{
    public class Announcement
    {
        static readonly string xmlDocumentPath = Student_App.Properties.Settings.Default.xmlDocumentPath;

        public enum subjects
        {
            School,
            Soccer,
            Volleyball,
            Basketball,
            General
        }

        public subjects subject;

        public string teacherName { get; set; }

        public string title { get; set; }

        public string date { get; set; }

        public string time { get; set; }

        public string deadline { get; set; }

        public string location { get; set; }

        public string description { get; set; }

        public string getSubject
        {
            get
            {
                return subject.ToString();
            }
        }

        public Announcement()
        {

        }

        public Announcement(string teacherName, string title, string date, string time, string deadline, string location, string description, string subject)
        {
            this.teacherName = teacherName;
            this.title = title;
            this.date = date;
            this.time = time;
            this.deadline = deadline;
            this.location = location;
            this.description = description;
            this.subject = (subjects) Enum.Parse(typeof(subjects), subject);
        }



        public static List<Announcement> getAnnouncements()
        {
            XmlDocument xDoc = new XmlDocument();
            if (System.IO.File.Exists(xmlDocumentPath))
            {
                xDoc.Load(xmlDocumentPath);

                XmlNode rootNode = xDoc.DocumentElement;
                List<Announcement> announcementsList = new List<Announcement>();

                foreach (XmlNode node in rootNode.SelectNodes(".//Announcement"))
                {
                    Announcement announcement = new Announcement(
                        node.SelectSingleNode(".//Teacher").InnerText,
                        node.SelectSingleNode(".//Title").InnerText,
                        node.SelectSingleNode(".//Date").InnerText,
                        node.SelectSingleNode(".//Time").InnerText,
                        node.SelectSingleNode(".//Deadline").InnerText,
                        node.SelectSingleNode(".//Location").InnerText,
                        node.SelectSingleNode(".//Description").InnerText,
                        node.SelectSingleNode(".//Subject").InnerText);

                    announcementsList.Add(announcement);
                }
                return announcementsList;
            }
            else
            {
                return new List<Announcement>();
            }
        }

        public static List<Announcement> getAnnouncements(string search)
        {
            if (System.IO.File.Exists(xmlDocumentPath))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(xmlDocumentPath);
                XmlNode rootNode = xDoc.DocumentElement;
                List<Announcement> announcementsList = new List<Announcement>();

                foreach (XmlNode node in rootNode.SelectNodes(".//Announcement"))
                {
                    Announcement announcement = new Announcement(
                        node.SelectSingleNode(".//Teacher").InnerText,
                        node.SelectSingleNode(".//Title").InnerText,
                        node.SelectSingleNode(".//Date").InnerText,
                        node.SelectSingleNode(".//Time").InnerText,
                        node.SelectSingleNode(".//Deadline").InnerText,
                        node.SelectSingleNode(".//Location").InnerText,
                        node.SelectSingleNode(".//Description").InnerText,
                        node.SelectSingleNode(".//Subject").InnerText);

                    search = search.ToLower();
                    if (announcement.teacherName.ToLower().Contains(search) || announcement.title.ToLower().Contains(search) || announcement.date.ToLower().Contains(search) || announcement.time.ToLower().Contains(search) || announcement.deadline.ToLower().Contains(search) || announcement.location.ToLower().Contains(search) || announcement.description.ToLower().Contains(search) || announcement.subject.ToString().ToLower().Contains(search))
                    {
                        announcementsList.Add(announcement);
                    }
                }
                return announcementsList;
            }
            else
            {
                return new List<Announcement>();
            }
        }

    }

}
