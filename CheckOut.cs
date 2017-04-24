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
    public partial class Check_Out : Form
    {
        public Check_Out()
        {
            InitializeComponent();

        }
        List<Patrons> people = new List<Patrons>();
        List<Book> books = new List<Book>();
        List<CheckOut> checkout = new List<CheckOut>();
        bool isUnckecked;
        bool whenUnckecked;
        bool isChecking;
        bool canCheck;
        void savetohistory()
        {
            try
            {


                if (!Directory.Exists("\\BookStore"))
                    Directory.CreateDirectory("\\BookStore");
                if (!File.Exists("\\BookStore\\CheckOutHistory.xml"))
                {
                    XmlTextWriter xW = new XmlTextWriter("\\BookStore\\CheckOutHistory.xml", Encoding.UTF8);
                    xW.WriteStartElement("Patrons");
                    xW.WriteEndElement();
                    xW.Close();
                }


                // this code saves everything after the closing the app. Problem was removing but somehow i add the same code for the remove button. It is working so i am not gonna look at it agian. We can check this later.
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("\\BookStore\\CheckOut.xml");
                XmlNode xNode = xDoc.SelectSingleNode("Patrons");
                
                foreach (CheckOut d in checkout)
                {
                    XmlNode xTopp = xDoc.CreateElement("Person");
                    XmlNode xName = xDoc.CreateElement("Name");
                    XmlNode xOwnBooks = xDoc.CreateElement("OwnBooks");
                    XmlNode xAllowedDays = xDoc.CreateElement("AllowedDays");
                    XmlNode xtook = xDoc.CreateElement("Took");
                    XmlNode xdue = xDoc.CreateElement("Due");

                    xName.InnerText = d.Name;
                    xOwnBooks.InnerText = d.OwnBooks;
                    xAllowedDays.InnerText = d.AllowedDays.ToString();
                    xtook.InnerText = d.took;
                    xdue.InnerText = d.due;

                    xTopp.AppendChild(xName);
                    xTopp.AppendChild(xOwnBooks);
                    xTopp.AppendChild(xAllowedDays);
                    xTopp.AppendChild(xtook);
                    xTopp.AppendChild(xdue);


                    xDoc.DocumentElement.AppendChild(xTopp);
                    xDoc.Save("\\BookStore\\CheckOutHistory.xml");
                }
            }
            catch { }
        }
        
        void checkingout()
        {
            try
            {
              
            
                if (!Directory.Exists("\\BookStore"))
                    Directory.CreateDirectory("\\BookStore");
                if (!File.Exists("\\BookStore\\CheckOut.xml"))
                {
                    XmlTextWriter xW = new XmlTextWriter("\\BookStore\\CheckOut.xml", Encoding.UTF8);
                    xW.WriteStartElement("Patrons");
                    xW.WriteEndElement();
                    xW.Close();
                }


                // this code saves everything after the closing the app. Problem was removing but somehow i add the same code for the remove button. It is working so i am not gonna look at it agian. We can check this later.
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("\\BookStore\\CheckOut.xml");
                XmlNode xNode = xDoc.SelectSingleNode("Patrons");
                
                foreach (CheckOut d in checkout)
                {
                    XmlNode xTopp = xDoc.CreateElement("Person");
                    XmlNode xName = xDoc.CreateElement("Name");
                    XmlNode xOwnBooks = xDoc.CreateElement("OwnBooks");
                    XmlNode xAllowedDays = xDoc.CreateElement("AllowedDays");
                    XmlNode xtook = xDoc.CreateElement("Took");
                    XmlNode xdue = xDoc.CreateElement("Due");

                    xName.InnerText = d.Name;
                    xOwnBooks.InnerText = d.OwnBooks;
                    xAllowedDays.InnerText = d.AllowedDays.ToString();
                    xtook.InnerText = d.took;
                    xdue.InnerText = d.due;

                    xTopp.AppendChild(xName);
                    xTopp.AppendChild(xOwnBooks);
                    xTopp.AppendChild(xAllowedDays);
                    xTopp.AppendChild(xtook);
                    xTopp.AppendChild(xdue);


                    xDoc.DocumentElement.AppendChild(xTopp);
                    xDoc.Save("\\BookStore\\CheckOut.xml");
                }
            }
            catch { }
        }


        private void Check_Out_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 14;
            comboBox2.SelectedIndex = 0;
            if (!Directory.Exists("\\BookStore"))
                Directory.CreateDirectory("\\BookStore");
            if (!File.Exists("\\BookStore\\CheckOut.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter("\\BookStore\\CheckOut.xml", Encoding.UTF8);
                xW.WriteStartElement("Patrons");
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
                listView2.Items.Add(list2);

            }
            
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load("\\BookStore\\books.xml");
            foreach (XmlNode xn in xdoc.SelectNodes("Books/Book"))
            {
                Book b = new Book();
                b.Title = xn.SelectSingleNode("Title").InnerText;
                b.Author = xn.SelectSingleNode("Author").InnerText;
                b.Genre = xn.SelectSingleNode("Genre").InnerText;
                b.ISBN = xn.SelectSingleNode("ISBN").InnerText;
                b.Copies = int.Parse(xn.SelectSingleNode("Copies").InnerText);
                books.Add(b);
                ListViewItem list = new ListViewItem(b.Title);
                if (b.Copies > 0)
                {
                    list.SubItems.Add(b.Copies.ToString());
                }
                else
                {
                    return;
                }

                listView1.Items.Add(list);




            }
            XmlDocument xdoc2 = new XmlDocument();
            xdoc2.Load("\\BookStore\\CheckOut.xml");
            foreach (XmlNode xn1 in xdoc2.SelectNodes("Patrons/Person"))
            {


                CheckOut c = new CheckOut();
                c.Name = xn1.SelectSingleNode("Name").InnerText;
                c.OwnBooks = xn1.SelectSingleNode("OwnBooks").InnerText;
                c.AllowedDays = int.Parse(xn1.SelectSingleNode("AllowedDays").InnerText);
                c.took = xn1.SelectSingleNode("Took").InnerText;
                c.due = xn1.SelectSingleNode("Due").InnerText;

                checkout.Add(c);



            }
            if (!Directory.Exists("\\BookStore"))
                Directory.CreateDirectory("\\BookStore");
            if (!File.Exists("\\BookStore\\CheckOutHistory.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter("\\BookStore\\CheckOutHistory.xml", Encoding.UTF8);
                xW.WriteStartElement("Patrons");
                xW.WriteEndElement();
                xW.Close();
            }
            



        }

        private void button2_Click(object sender, EventArgs e)
        {
            CheckOutHistory f2 = new CheckOutHistory();
            f2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                CheckOut c = new CheckOut();
                Book b = new Book();
                c.Name = listView2.CheckedItems[0].Text+" "+listView2.CheckedItems[0].SubItems[1].Text ;
                c.OwnBooks=listView1.CheckedItems[0].Text;
                c.AllowedDays = int.Parse(comboBox1.SelectedItem.ToString());
                c.due = dateTimePicker2.Text;
                c.took = dateTimePicker1.Text;
                checkout.Add(c);
                XmlDocument xdoc2 = new XmlDocument();
                xdoc2.Load("\\BookStore\\books.xml");
                foreach (XmlNode xn in xdoc2.SelectNodes("Books/Book"))
                {
                    if (xn.SelectSingleNode("Title").InnerText == c.OwnBooks)
                    {

                        b.Copies = int.Parse(xn.SelectSingleNode("Copies").InnerText);
                        b.Copies = b.Copies - 1;
                        xn.SelectSingleNode("Copies").InnerText = b.Copies.ToString();


                    }
                    
                }
                xdoc2.Save("\\BookStore\\books.xml");



                savetohistory();

                checkingout();

                 listView1.Items.Clear();
             
                XmlDocument xdoc = new XmlDocument();
                xdoc.Load("\\BookStore\\books.xml");
                foreach (XmlNode xn in xdoc.SelectNodes("Books/Book"))
                {

                    b.Title = xn.SelectSingleNode("Title").InnerText;



                    b.Copies = int.Parse(xn.SelectSingleNode("Copies").InnerText);
                    books.Add(b);
                    ListViewItem list = new ListViewItem(b.Title);
                    if (b.Copies > 0)
                    {
                        list.SubItems.Add(b.Copies.ToString());
                    }
                    else
                    {
                        return;
                    }


                    listView1.Items.Add(list);
                }
            }
            catch { }
        }

        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
            }

        
        

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (!listView1.GetItemAt(e.X, e.Y).Checked)
                {
                    canCheck = true;
                    listView1.GetItemAt(e.X, e.Y).Checked = true;
                }
                else
                    isUnckecked = true;
            }
            catch { }
        }

        private void AddDaysToDatePicker(int days, DateTimePicker startDTP, DateTimePicker endDTP)
        {
            endDTP.Value = startDTP.Value.AddDays(days);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int days = int.Parse(comboBox1.SelectedItem.ToString());
            AddDaysToDatePicker(days, dateTimePicker1, dateTimePicker2);
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            

           
        }

        

        private void listView1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isUnckecked)
            {
                isUnckecked = false;
                isChecking = true;
                listView1.Items[e.Index].Checked = false;
                e.NewValue = CheckState.Unchecked;

                isChecking = false;
                whenUnckecked = true;
            }
            if (!isChecking && canCheck)
            {
                isChecking = true;
                foreach (ListViewItem item in listView1.Items)
                {
                    item.Checked = false;
                }
                listView1.Items[e.Index].Checked = true;
                e.NewValue = CheckState.Checked;
                canCheck = false;
                isChecking = false;
            }
            else
            {
                if (isChecking || whenUnckecked)
                {
                    e.NewValue = CheckState.Unchecked;
                    whenUnckecked = false;

                }
                else
                {
                    e.NewValue = e.CurrentValue;

                }
            }
        }
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
        private int sortColumn = -1;
        
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                




                }
            
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.TextLength == 0)
                {
                    listView2.Items.Clear();//clear list


                    foreach (Patrons p in people)//instead of loading XML file again and again, loop for each person and Add to ListView
                    {

                        ListViewItem list2 = new ListViewItem(p.FirstName);
                        list2.SubItems.Add(p.LastName);
                        listView2.Items.Add(list2);
                    }
                }
                else
                {
                    int selection = comboBox2.SelectedIndex;//comboBox2 is drop down with First Name, Last ....etc
                    ArrayList results = new ArrayList();//initialize arraylist
                    for (int i = 0; i < listView2.Items.Count; i++)//loops rows of ListView1 
                    {
                        string a = listView2.Items[i].SubItems[selection].Text;//creates a string based on row i, column 'selection'
                        if (a.IndexOf(textBox2.Text, StringComparison.OrdinalIgnoreCase) != -1)//compares user types in textBox6 (search) ignores case
                        {
                            results.Add(listView2.Items[i]);
                        }

                    }
                    listView2.Items.Clear();
                    for (int i = 0; i < results.Count; i++)
                    {
                        listView2.Items.Add((ListViewItem)results[i]);
                    }
                }
            }
            catch { }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
                try
            {
                if (textBox1.TextLength == 0)
                {
                    listView1.Items.Clear();//clear list
                    // when we click manage people in main program, add books loads and start with creating xml file then fill it with information which come from class person and textboxes.
                    //comboBox1.SelectedIndex = 0;

                    foreach (Book b in books)//instead of loading XML file again and again, loop for each book and Add to ListView
                    {

                        ListViewItem list = new ListViewItem(b.Title);
                        list.SubItems.Add(b.Copies.ToString());
                        listView1.Items.Add(list);
                    }
                }
                else
                {
                    ListViewItem result = listView1.FindItemWithText(textBox1.Text, true, 0);//finditem with text method on ListView
                    ArrayList results = new ArrayList();
                    int currentindex = 0;
                    while (result != null)
                    {
                        results.Add(result);
                        currentindex = result.Index;
                        string a = listView1.Items[0].Text;
                        if (currentindex < listView1.Items.Count - 1)
                        {
                            result = listView1.FindItemWithText(textBox1.Text, true, currentindex + 1);
                        }
                        else
                            result = null;
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

       

        

       

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
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

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                if (!listView2.GetItemAt(e.X, e.Y).Checked)
                {
                    canCheck = true;
                    listView2.GetItemAt(e.X, e.Y).Checked = true;
                }
                else
                    isUnckecked = true;
            }
            catch { }
        }

        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
                // Set the sort order to ascending by default.
                listView2.Sorting = SortOrder.Ascending;
            }
            else
            {
                // Determine what the last sort order was and change it.
                if (listView2.Sorting == SortOrder.Ascending)
                    listView2.Sorting = SortOrder.Descending;
                else
                    listView2.Sorting = SortOrder.Ascending;
            }

            // Call the sort method to manually sort.
            listView2.Sort();
            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            this.listView2.ListViewItemSorter = new ListViewItemComparer(e.Column,
                                                              listView2.Sorting);
        }

        private void listView2_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isUnckecked)
            {
                isUnckecked = false;
                isChecking = true;
                listView2.Items[e.Index].Checked = false;
                e.NewValue = CheckState.Unchecked;

                isChecking = false;
                whenUnckecked = true;
            }
            if (!isChecking && canCheck)
            {
                isChecking = true;
                foreach (ListViewItem item in listView2.Items)
                {
                    item.Checked = false;
                }
                listView2.Items[e.Index].Checked = true;
                e.NewValue = CheckState.Checked;
                canCheck = false;
                isChecking = false;
            }
            else
            {
                if (isChecking || whenUnckecked)
                {
                    e.NewValue = CheckState.Unchecked;
                    whenUnckecked = false;

                }
                else
                {
                    e.NewValue = e.CurrentValue;

                }
            }
        }

       
        }
    }






