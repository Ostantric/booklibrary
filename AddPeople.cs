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
    public partial class AddPeople : Form
    {
        public AddPeople()
        {
            InitializeComponent();
        }
        List<Patrons> people = new List<Patrons>(); // listing people information

        private void label4_Click(object sender, EventArgs e)
        {

        }

    
        void repeat()
        {
            try
            {

                // this code saves everything after the closing the app. Problem was removing but somehow i add the same code for the remove button. It is working so i am not gonna look at it agian. We can check this later.
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("\\BookStore\\people.xml");
                XmlNode xNode = xDoc.SelectSingleNode("People");
                xNode.RemoveAll();
                foreach (Patrons p in people)
                {
                    XmlNode xTop = xDoc.CreateElement("Patrons");
                    XmlNode xFirstName = xDoc.CreateElement("FirstName");
                    XmlNode xLastName = xDoc.CreateElement("LastName");
                    XmlNode xAddress = xDoc.CreateElement("Address");
                    XmlNode xCity = xDoc.CreateElement("City");
                    XmlNode xState = xDoc.CreateElement("State");
                    XmlNode xZipcode = xDoc.CreateElement("Zipcode");
                    XmlNode xEmail = xDoc.CreateElement("Email");
                    XmlNode xPhone = xDoc.CreateElement("PhoneNum");
                    XmlNode xBirthday = xDoc.CreateElement("Birthday");
                    xFirstName.InnerText = p.FirstName;
                    xLastName.InnerText = p.LastName;
                    xAddress.InnerText = p.Address;
                    xCity.InnerText = p.City;
                    xState.InnerText = p.State;
                    xZipcode.InnerText = p.Zipcode;
                    xEmail.InnerText = p.Email;
                    xPhone.InnerText = p.PhoneNum;
                    xBirthday.InnerText = p.BirthDay;
                    xTop.AppendChild(xFirstName);
                    xTop.AppendChild(xLastName);
                    xTop.AppendChild(xAddress);
                    xTop.AppendChild(xCity);
                    xTop.AppendChild(xState);
                    xTop.AppendChild(xZipcode);
                    xTop.AppendChild(xEmail);
                    xTop.AppendChild(xPhone);
                    xTop.AppendChild(xBirthday);
                    xDoc.DocumentElement.AppendChild(xTop);
                    xDoc.Save("\\BookStore\\people.xml");
                }


                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                comboBox1.Text = "";

                dateTimePicker1.Value = DateTime.Now;
            }
            catch { }
        }

        private void AddPeople_Load(object sender, EventArgs e)
        {
            // when we click manage people in main program, add people loads and start with creating xml file then fill it with information which come from class Patrons and textboxes.
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            if (!Directory.Exists("\\BookStore"))
                Directory.CreateDirectory("\\BookStore");
            if (!File.Exists("\\BookStore\\people.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter("\\BookStore\\people.xml", Encoding.UTF8);
                xW.WriteStartElement("People");
                xW.WriteEndElement();
                xW.Close();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("\\BookStore\\people.xml");
            foreach (XmlNode xNode in xDoc.SelectNodes("People/Patrons"))
            {
                Patrons p = new Patrons();
                p.FirstName = xNode.SelectSingleNode("FirstName").InnerText;
                p.LastName = xNode.SelectSingleNode("LastName").InnerText;
                p.Address = xNode.SelectSingleNode("Address").InnerText;
                p.City = xNode.SelectSingleNode("City").InnerText;
                p.Zipcode = xNode.SelectSingleNode("Zipcode").InnerText;
                p.Email = xNode.SelectSingleNode("Email").InnerText;
                p.PhoneNum = xNode.SelectSingleNode("PhoneNum").InnerText;
                p.State = xNode.SelectSingleNode("State").InnerText;
                p.BirthDay = xNode.SelectSingleNode("Birthday").InnerText;
                people.Add(p);
                ListViewItem list2 = new ListViewItem(p.FirstName);
                list2.SubItems.Add(p.LastName);
                list2.SubItems.Add(p.Address);
                list2.SubItems.Add(p.City);
                list2.SubItems.Add(p.State);
                list2.SubItems.Add(p.Zipcode);
                list2.SubItems.Add(p.Email);
                list2.SubItems.Add(p.PhoneNum);
                list2.SubItems.Add(p.BirthDay);


                listView1.Items.Add(list2);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //This button adds text into list view then xml files takes it from list view and text boxes.
            Patrons p = new Patrons();
            p.FirstName = textBox1.Text;
            p.LastName = textBox2.Text;
            p.Address = textBox3.Text;
            p.City = textBox4.Text;
            p.Zipcode = textBox5.Text;
            p.Email = textBox6.Text;
            p.PhoneNum = textBox7.Text;
            p.State = comboBox1.Text;

            p.BirthDay = dateTimePicker1.Text;
            people.Add(p);
            ListViewItem list2 = new ListViewItem(p.FirstName);
            list2.SubItems.Add(p.LastName);
            list2.SubItems.Add(p.Address);
            list2.SubItems.Add(p.City);
            list2.SubItems.Add(p.State);
            list2.SubItems.Add(p.Zipcode);
            list2.SubItems.Add(p.Email);
            list2.SubItems.Add(p.PhoneNum);
            list2.SubItems.Add(p.BirthDay);


            listView1.Items.Add(list2);
            repeat();



        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                // this button changes the values from the Patrons who already exist.
                people[listView1.SelectedItems[0].Index].FirstName = textBox1.Text;
                people[listView1.SelectedItems[0].Index].LastName = textBox2.Text;
                people[listView1.SelectedItems[0].Index].Address = textBox3.Text;
                people[listView1.SelectedItems[0].Index].City = textBox4.Text;
                people[listView1.SelectedItems[0].Index].Zipcode = textBox5.Text;
                people[listView1.SelectedItems[0].Index].Email = textBox6.Text;
                people[listView1.SelectedItems[0].Index].PhoneNum = textBox7.Text;
                people[listView1.SelectedItems[0].Index].State = comboBox1.Text;
                people[listView1.SelectedItems[0].Index].BirthDay = dateTimePicker1.Text;



                repeat();
                listView1.Items.Clear();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("\\BookStore\\people.xml");
                foreach (XmlNode xNode in xDoc.SelectNodes("People/Patrons"))
                {
                    Patrons p = new Patrons();
                    p.FirstName = xNode.SelectSingleNode("FirstName").InnerText;
                    p.LastName = xNode.SelectSingleNode("LastName").InnerText;
                    p.Address = xNode.SelectSingleNode("Address").InnerText;
                    p.City = xNode.SelectSingleNode("City").InnerText;
                    p.Zipcode = xNode.SelectSingleNode("Zipcode").InnerText;
                    p.Email = xNode.SelectSingleNode("Email").InnerText;
                    p.PhoneNum = xNode.SelectSingleNode("PhoneNum").InnerText;
                    p.State = xNode.SelectSingleNode("State").InnerText;
                    p.BirthDay = xNode.SelectSingleNode("Birthday").InnerText;
                    people.Add(p);
                    ListViewItem list2 = new ListViewItem(p.FirstName);
                    list2.SubItems.Add(p.LastName);
                    list2.SubItems.Add(p.Address);
                    list2.SubItems.Add(p.City);
                    list2.SubItems.Add(p.State);
                    list2.SubItems.Add(p.Zipcode);
                    list2.SubItems.Add(p.Email);
                    list2.SubItems.Add(p.PhoneNum);
                    list2.SubItems.Add(p.BirthDay);


                    listView1.Items.Add(list2);
                }
            
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // this was the hardest because deleting elements from xml files is kind of complicated anyway remove funciton defined and it works here. 
            //Then this button finds the exact FirstName in the xml file and deletes the all information by using if loop.
            //I use some bypass, here is my solution. So remember when u click on a Patrons in the view list you see full information on the screen. So i was thinking how can i delete the exact Patrons from the xml.
            // Then i find this bypass that can work. In short if loop checks the textbox1 has a value or not and no matter what happens everytime there will be a value when u click on a Patrons on the list
            //after checking the textbox1, if loop deletes the all information under the FirstName and then code saves to the xml file.

            DialogResult result = MessageBox.Show("Do you really want to delete this ?", string.Empty, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Remove();
            }
            else
            {
                return;
            }
   
            
            
            
            
            try
            {

                XmlDocument xdoc = new XmlDocument();
                xdoc.Load("\\BookStore\\people.xml");
                foreach (XmlNode node in xdoc.SelectNodes("People/Patrons"))
                {


                    if (node.SelectSingleNode("FirstName").InnerText == textBox1.Text)
                    {
                        node.ParentNode.RemoveChild(node);
                    }


                }

                xdoc.Save("\\BookStore\\people.xml");
            }
            catch { }
            textBox1.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }
        void Remove()
        {
            // this function deletes the information just for the event not for the storage. I couldnt make a full function for both(i mean xml and right away)
            try
            {

                listView1.Items.Remove(listView1.SelectedItems[0]);
                people.RemoveAt(listView1.SelectedItems[0].Index);



            }
            catch { }
        }


        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }


        private void AddPeople_FormClosing(object sender, FormClosingEventArgs e)
        {



            




        }
        private int sortColumn = -1;

        class ListViewItemComparer : IComparer
        {
            private int col;
            private SortOrder order;
            public ListViewItemComparer()
            {
                col = 0;
                order = SortOrder.Ascending;
            }
            public ListViewItemComparer(int column, SortOrder order)
            {
                col = column;
                this.order = order;
            }
            public int Compare(object x, object y)
            {
                int returnVal = -1;
                returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
                                        ((ListViewItem)y).SubItems[col].Text);
                // Determine whether the sort order is descending.
                if (order == SortOrder.Descending)
                    // Invert the value returned by String.Compare.
                    returnVal *= -1;
                return returnVal;
            }
        }









        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // this is list of people and their information which keep in the list. List only shows their full FirstName, when you click on the FirstName you will see the full details of the Patrons.
            try
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
                textBox5.Text = listView1.SelectedItems[0].SubItems[5].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[4].Text;
                textBox6.Text = listView1.SelectedItems[0].SubItems[6].Text;
                textBox7.Text = listView1.SelectedItems[0].SubItems[7].Text;
                dateTimePicker1.Text = listView1.SelectedItems[0].SubItems[8].Text;

            }
            catch { }
        }

        private void listView1_ColumnClick_1(object sender, ColumnClickEventArgs e)
        {

            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listView1.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              listView1.Sorting);

        }

        private void textBox5_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox41_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();//clear list
            // when we click manage people in main program, add people loads and start with creating xml file then fill it with information which come from class person and textboxes.
            //comboBox1.SelectedIndex = 0;

            foreach (Patrons p in people)//instead of loading XML file again and again, loop for each person and Add to ListView
            {

                ListViewItem list2 = new ListViewItem(p.FirstName);
                list2.SubItems.Add(p.LastName);
                list2.SubItems.Add(p.Address);
                list2.SubItems.Add(p.City);
                list2.SubItems.Add(p.State);
                list2.SubItems.Add(p.Zipcode);
                list2.SubItems.Add(p.Email);
                list2.SubItems.Add(p.PhoneNum);
                list2.SubItems.Add(p.BirthDay);


                listView1.Items.Add(list2);
            }
        }

        
        private void button6_Click(object sender, EventArgs e)
        {
            int selection = comboBox2.SelectedIndex;//comboBox2 is drop down with First Name, Last ....etc
            ArrayList results = new ArrayList();//initialize arraylist
            for (int i = 0; i < listView1.Items.Count; i++)//loops rows of ListView1 
            {
                string a = listView1.Items[i].SubItems[selection].Text;//creates a string based on row i, column 'selection'
                if (a.IndexOf(textBox9.Text, StringComparison.OrdinalIgnoreCase) != -1)//compares user types in textBox6 (search) ignores case
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

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox9.TextLength == 0)
                {

                    listView1.Items.Clear();//clear list
                    // when we click manage people in main program, add people loads and start with creating xml file then fill it with information which come from class person and textboxes.
                    //comboBox1.SelectedIndex = 0;

                    foreach (Patrons p in people)//instead of loading XML file again and again, loop for each person and Add to ListView
                    {

                        ListViewItem list2 = new ListViewItem(p.FirstName);
                        list2.SubItems.Add(p.LastName);
                        list2.SubItems.Add(p.Address);
                        list2.SubItems.Add(p.City);
                        list2.SubItems.Add(p.State);
                        list2.SubItems.Add(p.Zipcode);
                        list2.SubItems.Add(p.Email);
                        list2.SubItems.Add(p.PhoneNum);
                        list2.SubItems.Add(p.BirthDay);


                        listView1.Items.Add(list2);
                    }
                }
                else
                {
                    int selection = comboBox2.SelectedIndex;//comboBox2 is drop down with First Name, Last ....etc
                    ArrayList results = new ArrayList();//initialize arraylist
                    for (int i = 0; i < listView1.Items.Count; i++)//loops rows of ListView1 
                    {
                        string a = listView1.Items[i].SubItems[selection].Text;//creates a string based on row i, column 'selection'
                        if (a.IndexOf(textBox9.Text, StringComparison.OrdinalIgnoreCase) != -1)//compares user types in textBox6 (search) ignores case
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