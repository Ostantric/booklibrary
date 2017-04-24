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
    public partial class Checkin : Form
    {
        public Checkin()
        {
            InitializeComponent();
        }
        List<Patrons> people = new List<Patrons>();
        List<Book> books = new List<Book>();
        List<CheckOut> checkout = new List<CheckOut>();
        List<Checkedin> checkin = new List<Checkedin>();
        bool isUnckecked;
        bool whenUnckecked;
        bool isChecking;
        bool canCheck;

        private void CheckIn_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
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
                listView2.Items.Add(list2);

            }



            if (!Directory.Exists("\\BookStore"))
                Directory.CreateDirectory("\\BookStore");
            if (!File.Exists("\\BookStore\\CheckOut.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter("\\BookStore\\CheckOut.xml", Encoding.UTF8);
                xW.WriteStartElement("Patrons");
                xW.WriteEndElement();
                xW.Close();
            }
        }





        
            
                


         







        
        void checkinbook()
        {
            Checkedin i = new Checkedin();
           
            
            
            if (!Directory.Exists("\\BookStore"))
                            Directory.CreateDirectory("\\BookStore");
                        if (!File.Exists("\\BookStore\\CheckIn.xml"))
                        {
                            XmlTextWriter xW = new XmlTextWriter("\\BookStore\\CheckIn.xml", Encoding.UTF8);
                            xW.WriteStartElement("Patrons");
                            xW.WriteEndElement();
                            xW.Close();
                        }
                        Book b = new Book();
                        CheckOut c = new CheckOut();
                        

                        c.Name = listView2.CheckedItems[0].Text + " " + listView2.CheckedItems[0].SubItems[1].Text;
                        c.OwnBooks = listView1.CheckedItems[0].Text;
                        i.Name = listView2.CheckedItems[0].Text + " " + listView2.CheckedItems[0].SubItems[1].Text;
                        i.Book = listView1.CheckedItems[0].Text;
                        i.CheckedOut = dateTimePicker2.Text;
                        i.CheckedIn = dateTimePicker1.Text;
                        if (DateTime.Parse(i.CheckedIn) > DateTime.Parse(i.CheckedOut))
                        {
                            i.Overdue = "Yes";
                        }
                        else
                        {
                            i.Overdue = "No";
                        }
                        checkin.Add(i);
                       
                        // this code saves everything after the closing the app. Problem was removing but somehow i add the same code for the remove button. It is working so i am not gonna look at it agian. We can check this later.
                        XmlDocument xDoc2 = new XmlDocument();
                        xDoc2.Load("\\BookStore\\CheckIn.xml");
                        XmlNode xNode1 = xDoc2.SelectSingleNode("Patrons");
                        
                        foreach (Checkedin d in checkin)
                        {
                            XmlNode xTopp = xDoc2.CreateElement("Person");
                            XmlNode xName = xDoc2.CreateElement("Name");
                            XmlNode xBook = xDoc2.CreateElement("Book");
                            XmlNode xCheckedOut = xDoc2.CreateElement("CheckedOut");
                            XmlNode xCheckedIn = xDoc2.CreateElement("CheckedIn");
                            XmlNode xOverdue = xDoc2.CreateElement("Overdue");

                            xName.InnerText = d.Name;
                            xBook.InnerText = d.Book;
                            xCheckedOut.InnerText = d.CheckedOut;
                            xCheckedIn.InnerText = d.CheckedIn;
                            xOverdue.InnerText = d.Overdue;

                            xTopp.AppendChild(xName);
                            xTopp.AppendChild(xBook);
                            xTopp.AppendChild(xCheckedOut);
                            xTopp.AppendChild(xCheckedIn);
                            xTopp.AppendChild(xOverdue);


                            xDoc2.DocumentElement.AppendChild(xTopp);
                            xDoc2.Save("\\BookStore\\CheckIn.xml");
                        }
                        
                        
                        
                        

                        XmlDocument xdoc3 = new XmlDocument();
                        xdoc3.Load("\\BookStore\\books.xml");
                        foreach (XmlNode xn in xdoc3.SelectNodes("Books/Book"))
                        {
                            if (xn.SelectSingleNode("Title").InnerText == c.OwnBooks)
                            {

                                b.Copies = int.Parse(xn.SelectSingleNode("Copies").InnerText);
                                b.Copies = b.Copies + 1;
                                xn.SelectSingleNode("Copies").InnerText = b.Copies.ToString();
                            }


                        }
                        xdoc3.Save("\\BookStore\\books.xml");


                        XmlDocument xdoc4 = new XmlDocument();
                        xdoc4.Load("\\BookStore\\CheckOut.xml");
                        foreach (XmlNode xn in xdoc4.SelectNodes("Patrons/Person"))
                        {
                            if (xn.SelectSingleNode("Name").InnerText == c.Name)
                            {
                                if (xn.SelectSingleNode("OwnBooks").InnerText == c.OwnBooks)
                                {
                                    xn.ParentNode.RemoveChild(xn);
                                }
                            }


                        }
                        xdoc4.Save("\\BookStore\\CheckOut.xml");

                        listView1.Items.Clear();

                        XmlDocument xdoc = new XmlDocument();
                        xdoc.Load("\\BookStore\\CheckOut.xml");

                        foreach (XmlNode xn in xdoc.SelectNodes("Patrons/Person"))
                        {
                            if (xn.SelectSingleNode("Name").InnerText == c.Name)
                            {
                                c.OwnBooks = xn.SelectSingleNode("OwnBooks").InnerText;




                                checkout.Add(c);
                                ListViewItem list = new ListViewItem(c.OwnBooks);



                                listView1.Items.Add(list);
                            }
                        }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimePicker1.Value >= dateTimePicker2.Value)
                {
                    DialogResult result = MessageBox.Show("This Book is overdue. Hit yes for charge this patron, hit no for ignore.", string.Empty, MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                     {
                         return;

                     }
                    else
                    {
                        checkinbook();
                       
                        

                     }       
                }
                else
                    {

                        checkinbook();
                        
                        }
                
                
                

            
                

                
         
        }
            catch { }



        }

        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {

                listView1.Items.Clear();
                CheckOut b = new CheckOut();
                b.Name = listView2.CheckedItems[0].Text + " " + listView2.CheckedItems[0].SubItems[1].Text;


                XmlDocument xdoc2 = new XmlDocument();
                xdoc2.Load("\\BookStore\\CheckOut.xml");
                foreach (XmlNode xn in xdoc2.SelectNodes("Patrons/Person"))
                {
                    if (xn.SelectSingleNode("Name").InnerText == b.Name)
                    {

                        b.OwnBooks = xn.SelectSingleNode("OwnBooks").InnerText;

                        listView1.Items.Add(b.OwnBooks);




                    }



                }
                
            
            }
            catch { }
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

        private void listView2_ItemActivate(object sender, EventArgs e)
        {
            
        }

        private void listView2_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            
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
        
        
        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {

                
                CheckOut b = new CheckOut();
                b.Name = listView2.CheckedItems[0].Text + " " + listView2.CheckedItems[0].SubItems[1].Text;
                b.OwnBooks = listView1.CheckedItems[0].Text;
                checkout.Add(b);

                XmlDocument xdoc2 = new XmlDocument();
                xdoc2.Load("\\BookStore\\CheckOut.xml");
                foreach (XmlNode xn in xdoc2.SelectNodes("Patrons/Person"))
                {
                    if (xn.SelectSingleNode("Name").InnerText == b.Name)
                    {
                        
                        if (xn.SelectSingleNode("OwnBooks").InnerText == b.OwnBooks)
                        {
                            b.due=xn.SelectSingleNode("Due").InnerText;
                            b.took = xn.SelectSingleNode("Took").InnerText;
                            
                            dateTimePicker2.Value = DateTime.Parse(b.due);
                            dateTimePicker3.Value = DateTime.Parse(b.took);
                            
                            b.AllowedDays= int.Parse(xn.SelectSingleNode("Allowed").InnerText);
                            checkout.Add(b);
                            
                            
                            
       
                        }
                        
                        




                    }



                }
                


            }
            catch { }

                    


                    



               
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
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

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ListViewItem result = listView2.FindItemWithText(textBox2.Text, true, 0);//finditem with text method on ListView
                ArrayList results = new ArrayList();
                int currentindex = 0;
                while (result != null)
                {
                    results.Add(result);
                    currentindex = result.Index;
                    string a = listView2.Items[0].Text;
                    if (currentindex < listView2.Items.Count - 1)
                    {
                        result = listView2.FindItemWithText(textBox2.Text, true, currentindex + 1);
                    }
                    else
                        result = null;
                }


                listView2.Items.Clear();

                for (int i = 0; i < results.Count; i++)
                {
                    listView2.Items.Add((ListViewItem)results[i]);
                }
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();//clear list
            

            foreach (Patrons p in people)//instead of loading XML file again and again, loop for each person and Add to ListView
            {

                ListViewItem list2 = new ListViewItem(p.FirstName + " " + p.LastName);
                listView2.Items.Add(list2);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
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
            catch { }
        }


        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                listView1.Items.Clear();//clear list
                // when we click manage people in main program, add people loads and start with creating xml file then fill it with information which come from class person and textboxes.
                //comboBox1.SelectedIndex = 0;
                CheckOut b = new CheckOut();
                b.Name = listView2.SelectedItems[0].Text;


                XmlDocument xdoc2 = new XmlDocument();
                xdoc2.Load("\\BookStore\\CheckOut.xml");
                foreach (XmlNode xn in xdoc2.SelectNodes("Patrons/Person"))
                {
                    if (xn.SelectSingleNode("Name").InnerText == b.Name)
                    {

                        b.OwnBooks = xn.SelectSingleNode("OwnBooks").InnerText;

                        listView1.Items.Add(b.OwnBooks);




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
                    // when we click manage people in main program, add people loads and start with creating xml file then fill it with information which come from class person and textboxes.
                    //comboBox1.SelectedIndex = 0;
                    CheckOut b = new CheckOut();
                    b.Name = listView2.CheckedItems[0].Text+" "+listView2.CheckedItems[0].SubItems[1].Text;


                    XmlDocument xdoc2 = new XmlDocument();
                    xdoc2.Load("\\BookStore\\CheckOut.xml");
                    foreach (XmlNode xn in xdoc2.SelectNodes("Patrons/Person"))
                    {
                        if (xn.SelectSingleNode("Name").InnerText == b.Name)
                        {

                            b.OwnBooks = xn.SelectSingleNode("OwnBooks").InnerText;

                            listView1.Items.Add(b.OwnBooks);




                        }
                    }
                }
                else
                {
                    int selection = comboBox1.SelectedIndex;//comboBox2 is drop down with First Name, Last ....etc
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                


                
                if (textBox2.TextLength == 0)
                {
                    listView2.Items.Clear();//clear list



                    XmlDocument xdoc2 = new XmlDocument();
                    xdoc2.Load("\\BookStore\\people.xml");
                    foreach (XmlNode xn in xdoc2.SelectNodes("People/Patrons"))
                    {
                        Patrons b = new Patrons();
                        b.FirstName = xn.SelectSingleNode("FirstName").InnerText;
                        b.LastName = xn.SelectSingleNode("LastName").InnerText;

                        people.Add(b);
                        ListViewItem list2 = new ListViewItem(b.FirstName);
                        list2.SubItems.Add(b.LastName);
                        listView2.Items.Add(list2);
                    }






                }
                else
                {
                    int selection = comboBox1.SelectedIndex;//comboBox2 is drop down with First Name, Last ....etc
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

        private void Checkin_CursorChanged(object sender, EventArgs e)
        {

        }

        private void Checkin_Activated(object sender, EventArgs e)
        {
            
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            CheckInHistory f2 = new CheckInHistory();
            f2.ShowDialog();
        }

        
    }
}

