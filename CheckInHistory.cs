using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Collections;

namespace BookStoreProject
{
    public partial class CheckInHistory : Form
    {
        public CheckInHistory()
        {
            InitializeComponent();
        }
        List<Checkedin> checkin = new List<Checkedin>();

        private void CheckInHistory_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            if (!Directory.Exists("\\BookStore"))
                Directory.CreateDirectory("\\BookStore");
            if (!File.Exists("\\BookStore\\CheckOutHistory.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter("\\BookStore\\CheckOutHistory.xml", Encoding.UTF8);
                xW.WriteStartElement("Patrons");
                xW.WriteEndElement();
                xW.Close();
            }
            XmlDocument xdoc2 = new XmlDocument();
            xdoc2.Load("\\BookStore\\Checkin.xml");
            foreach (XmlNode xn1 in xdoc2.SelectNodes("Patrons/Person"))
            {
                try
                {

                    Checkedin c = new Checkedin();
                    c.Name = xn1.SelectSingleNode("Name").InnerText;
                    c.Book = xn1.SelectSingleNode("Book").InnerText;
                    c.CheckedOut = xn1.SelectSingleNode("CheckedOut").InnerText;
                    c.CheckedIn= xn1.SelectSingleNode("CheckedIn").InnerText;
                    c.Overdue= xn1.SelectSingleNode("Overdue").InnerText;

                    checkin.Add(c);
                    ListViewItem list1 = new ListViewItem(c.Name);
                    list1.SubItems.Add(c.Book);
                    list1.SubItems.Add(c.CheckedIn);
                    list1.SubItems.Add(c.CheckedOut);
                    list1.SubItems.Add(c.Overdue);
                    listView1.Items.Add(list1);
                }
                catch { }

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.TextLength == 0)
                {
                    listView1.Items.Clear();//clear list


                    XmlDocument xdoc2 = new XmlDocument();
                    xdoc2.Load("\\BookStore\\Checkin.xml");
                    foreach (XmlNode xn1 in xdoc2.SelectNodes("Patrons/Person"))
                    {
                        Checkedin c = new Checkedin();
                        c.Name = xn1.SelectSingleNode("Name").InnerText;
                        c.Book = xn1.SelectSingleNode("Book").InnerText;
                        c.CheckedOut = xn1.SelectSingleNode("CheckedOut").InnerText;
                        c.CheckedIn = xn1.SelectSingleNode("CheckedIn").InnerText;
                        c.Overdue = xn1.SelectSingleNode("Overdue").InnerText;
                        checkin.Add(c);
                        ListViewItem list1 = new ListViewItem(c.Name);
                        list1.SubItems.Add(c.Book);
                        list1.SubItems.Add(c.CheckedIn);
                        list1.SubItems.Add(c.CheckedOut);
                        list1.SubItems.Add(c.Overdue);
                        listView1.Items.Add(list1);
                    }






                }
                else
                {
                    int selection = comboBox1.SelectedIndex;//comboBox1 is drop down with First Name, Last ....etc
                    ArrayList results = new ArrayList();//initialize arraylist
                    for (int i = 0; i < listView1.Items.Count; i++)//loops rows of ListView1 
                    {
                        string a = listView1.Items[i].SubItems[selection].Text;//creates a string based on row i, column 'selection'
                        if (a.IndexOf(textBox2.Text, StringComparison.OrdinalIgnoreCase) != -1)//compares user types in textBox6 (search) ignores case
                        {
                            results.Add(listView1.Items[i]);
                        }

                    }
                    listView1.Items.Clear();
                    for (int i = 0; i < results.Count; i++)
                    {
                        listView1.Items.Add((ListViewItem)results[i]);
                    }

                }
            }
            catch { }
        }
    }
}
