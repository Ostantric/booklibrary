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
    public partial class CheckOutHistory : Form
    {
        public CheckOutHistory()
        {
            InitializeComponent();
        }
        List<CheckOut> checkout = new List<CheckOut>();
        
        
        private void CheckOutHistory_Load(object sender, EventArgs e)
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
            xdoc2.Load("\\BookStore\\CheckOutHistory.xml");
            foreach (XmlNode xn1 in xdoc2.SelectNodes("Patrons/Person"))
            {
                try
                {

                    CheckOut c = new CheckOut();
                    c.Name = xn1.SelectSingleNode("Name").InnerText;
                    c.OwnBooks = xn1.SelectSingleNode("OwnBooks").InnerText;
                    c.AllowedDays = int.Parse(xn1.SelectSingleNode("AllowedDays").InnerText);
                    c.took = xn1.SelectSingleNode("Took").InnerText;
                    c.due = xn1.SelectSingleNode("Due").InnerText;

                    checkout.Add(c);
                    ListViewItem list1 = new ListViewItem(c.Name);
                    list1.SubItems.Add(c.OwnBooks);
                    list1.SubItems.Add(c.AllowedDays.ToString());
                    list1.SubItems.Add(c.took);
                    list1.SubItems.Add(c.due);
                    listView1.Items.Add(list1);
                }
                catch { }

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.TextLength == 0)
                {
                    listView1.Items.Clear();//clear list



                    XmlDocument xdoc2 = new XmlDocument();
                    xdoc2.Load("\\BookStore\\CheckOutHistory.xml");
                    foreach (XmlNode xn1 in xdoc2.SelectNodes("Patrons/Person"))
                    {
                        CheckOut c = new CheckOut();
                        c.Name = xn1.SelectSingleNode("Name").InnerText;
                        c.OwnBooks = xn1.SelectSingleNode("OwnBooks").InnerText;
                        c.AllowedDays = int.Parse(xn1.SelectSingleNode("AllowedDays").InnerText);
                        c.took = xn1.SelectSingleNode("Took").InnerText;
                        c.due = xn1.SelectSingleNode("Due").InnerText;

                        checkout.Add(c);
                        ListViewItem list1 = new ListViewItem(c.Name);
                        list1.SubItems.Add(c.OwnBooks);
                        list1.SubItems.Add(c.AllowedDays.ToString());
                        list1.SubItems.Add(c.took);
                        list1.SubItems.Add(c.due);
                        listView1.Items.Add(list1);
                    }






                }
                else
                {
                    int selection = comboBox1.SelectedIndex;//comboBox2 is drop down with First Name, Last ....etc
                    ArrayList results = new ArrayList();//initialize arraylist
                    for (int i = 0; i < listView1.Items.Count; i++)//loops rows of ListView1 
                    {
                        string a = listView1.Items[i].SubItems[selection].Text;//creates a string based on row i, column 'selection'
                        if (a.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) != -1)//compares user types in textBox6 (search) ignores case
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

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}


