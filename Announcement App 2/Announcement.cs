using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Xml;
using System.Net.Http;

namespace Announcement_App_2
{
    public class Announcement
    {
        static readonly string xmlDocumentPath = Announcement_App_2.Properties.Settings.Default.xmlDocumentPath;
        static readonly string connectionString = Announcement_App_2.Properties.Settings.Default.databaseConnectionString;

        public enum subjects
        {
            School,
            Soccer,
            Volleyball,
            Basketball,
            Baseball,
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
            try
            {
                this.subject = (subjects)Enum.Parse(typeof(subjects), subject);
            }
            catch (Exception)
            {

                this.subject = subjects.General;
            }

        }


        #region Xml
  /*      public static int deleteAnnouncement(Announcement announcement, bool showMsgBoxes)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(xmlDocumentPath);
                XmlNode rootNode = xDoc.DocumentElement;
                foreach (XmlNode node in rootNode.SelectNodes(".//Announcement"))
                {
                    Announcement tempAnnouncement = new Announcement(
                        node.SelectSingleNode(".//Teacher").InnerText,
                        node.SelectSingleNode(".//Title").InnerText,
                        node.SelectSingleNode(".//Date").InnerText,
                        node.SelectSingleNode(".//Time").InnerText,
                        node.SelectSingleNode(".//Deadline").InnerText,
                        node.SelectSingleNode(".//Location").InnerText,
                        node.SelectSingleNode(".//Description").InnerText,
                        node.SelectSingleNode(".//Subject").InnerText);

                    if (tempAnnouncement.title == announcement.title)
                    {
                        xDoc.DocumentElement.RemoveChild(node);
                    }
                }
                xDoc.Save(xmlDocumentPath);
                if (showMsgBoxes)
                {
                    MessageBox.Show("Announcement Removed Successfully.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 1;
            }
        }


        public static int addAnnouncement(Announcement announcement, bool showMsgBoxes)
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (System.IO.File.Exists(xmlDocumentPath))
            {
                xmlDoc.Load(xmlDocumentPath);
            }
            else
            {
                System.IO.Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\NPSSAnnouncements");
                System.IO.FileStream file = System.IO.File.Create(xmlDocumentPath);
                file.Close();
                XmlNode announcementsNode = xmlDoc.CreateElement("Announcements");
                xmlDoc.AppendChild(announcementsNode);
            }

            XmlNode announcementNode = xmlDoc.CreateElement("Announcement");
            XmlNode titleNode = xmlDoc.CreateElement("Title");
            XmlNode dateNode = xmlDoc.CreateElement("Date");
            XmlNode timeNode = xmlDoc.CreateElement("Time");
            XmlNode subjectNode = xmlDoc.CreateElement("Subject");
            XmlNode descriptionNode = xmlDoc.CreateElement("Description");
            XmlNode locationNode = xmlDoc.CreateElement("Location");
            XmlNode deadlineNode = xmlDoc.CreateElement("Deadline");
            XmlNode nameNode = xmlDoc.CreateElement("Teacher");

            locationNode.InnerText = announcement.location;
            deadlineNode.InnerText = announcement.deadline;
            titleNode.InnerText = announcement.title;
            dateNode.InnerText = announcement.date;
            timeNode.InnerText = announcement.time;
            subjectNode.InnerText = announcement.subject.ToString();
            descriptionNode.InnerText = announcement.description;
            nameNode.InnerText = announcement.teacherName;

            announcementNode.AppendChild(nameNode);
            announcementNode.AppendChild(titleNode);
            announcementNode.AppendChild(dateNode);
            announcementNode.AppendChild(timeNode);
            announcementNode.AppendChild(subjectNode);
            announcementNode.AppendChild(descriptionNode);
            announcementNode.AppendChild(deadlineNode);
            announcementNode.AppendChild(locationNode);

            xmlDoc.DocumentElement.AppendChild(announcementNode);

            try
            {
                xmlDoc.Save(xmlDocumentPath);
                if (showMsgBoxes)
                {
                    MessageBox.Show("Announcement added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 1;
            }
        }



        public static List<Announcement> getAnnouncements()
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
        } */
        #endregion



        public static int deleteAnnouncementFromDatabase(Announcement announcement, bool showMsgBoxes)
        {
            XmlDocument xDoc = new XmlDocument();
            try
            {
                MySql.Data.MySqlClient.MySqlConnection mySqlCon = new MySql.Data.MySqlClient.MySqlConnection();
                mySqlCon.ConnectionString = connectionString;
                mySqlCon.Open();

                MySql.Data.MySqlClient.MySqlCommand command = new MySql.Data.MySqlClient.MySqlCommand();
                command.Connection = mySqlCon;

                command.CommandText = "delete";
                if (showMsgBoxes)
                {
                    MessageBox.Show("Announcement Removed Successfully.", "Success!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 1;
            }
        }





        public static int addAnnouncementToDatabase(Announcement announcement, bool showMsgBoxes)
        {
            try {
                HttpClient httpPost = new System.Net.Http.HttpClient();

                var values = new Dictionary<string, string>
            {
                { "title", announcement.title },
                { "teacherName", announcement.teacherName },
                { "subject", announcement.getSubject },
                { "content", announcement.description },
                { "location", announcement.location },
                { "deadline", announcement.deadline },
            };

                var data = new FormUrlEncodedContent(values);
                httpPost.PostAsync("https://npssannouncements.000webhostapp.com//create//addAnnouncement.php", data);

                if (showMsgBoxes)
                {
                    MessageBox.Show("Announcement added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                return 0;
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return 1;
            }

        }




        public static List<Announcement> getAnnouncementsFromDatabase()
        {
            string httpResponse = getRequest();

            List<Announcement> announcementList = new List<Announcement>();
            httpResponse = httpResponse.Replace("[", "").Replace("]", "");

            foreach (string announcementString in getSubstrings(httpResponse, "{", "}"))
            {

                string editedAnnouncement = announcementString.Replace(",", "").Replace("[", "").Replace("]","").Replace(":", "").Replace("announcementID","").Replace("title", "").Replace("teacherName", "").Replace("subjectOf", "").Replace("content", "").Replace("location", "").Replace("dateCreated", "").Replace("deadline", "").Replace("timeCreated", "");

                Announcement announcement = new Announcement();

                List<string> announcementProperties = getSubstrings(announcementString, "\"", 6, 4);

                DateTime time = new DateTime(1, 2, 3, Convert.ToInt32(announcementProperties[5].Substring(11, 2)), Convert.ToInt32(announcementProperties[5].Substring(14, 2)), 1);

                announcementList.Add(new Announcement(
                    announcementProperties[1],
                    announcementProperties[0],
                    announcementProperties[5].Substring(0,10),
                    time.ToShortTimeString(),
                    announcementProperties[6],
                    announcementProperties[4],
                    announcementProperties[3],
                    announcementProperties[2]));
                }
            return announcementList;

        }


        public static string getRequest()
        {
            try
            {
                HttpClient httpGet = new System.Net.Http.HttpClient();

                Task<string> response = httpGet.GetStringAsync("https://npssannouncements.000webhostapp.com//view//getAnnouncements.php");

                return response.Result;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }


        public static List<string> getSubstrings(string mainString, string seperator, int startIndex, int indexIncrement)
        {
            List<int> seperatorIndexes = new List<int>();
            List<string> substrings = new List<string>();

            for (int i = 0; i < mainString.Length; i++)
            {
                if (mainString.Substring(i, 1) == seperator)
                {
                    seperatorIndexes.Add(i);
                }
            }

            for (int i = startIndex; i < seperatorIndexes.Count; i += indexIncrement)
            {
                substrings.Add(mainString.Substring(seperatorIndexes[i] + 1, (seperatorIndexes[i + 1] - 1) - seperatorIndexes[i]));
            }

            return substrings;
        }


        public static List<string> getSubstrings(string mainString, string seperator1, string seperator2)
        {
            List<int> firstSeperatorIndexes = new List<int>();
            List<int> secondSeperatorIndexes = new List<int>();
            List<string> substrings = new List<string>();

            for (int i = 0; i < mainString.Length; i++)
            {
                if (mainString.Substring(i, 1) == seperator1)
                {
                    firstSeperatorIndexes.Add(i);
                }
                else if (mainString.Substring(i,1) == seperator2)
                {
                    secondSeperatorIndexes.Add(i);
                }
            }

            for (int i = 0; i < firstSeperatorIndexes.Count; i++)
            {
                substrings.Add(mainString.Substring(firstSeperatorIndexes[i] + 1, (secondSeperatorIndexes[i] - 1) - firstSeperatorIndexes[i]));
            }

            return substrings;
        }



        public static List<Announcement> getAnnouncements(string search)
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


        public static int deleteAnnouncement(Announcement announcement, bool showMsgBoxes)
        {
            return 1;

        }



    }

}
