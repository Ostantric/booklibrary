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
using System.Threading;


namespace BookStoreProject
{
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }
        List<Book> books = new List<Book>(); // listing books
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


        


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        void repeat()
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load("\\BookStore\\books.xml");
                XmlNode xn = xDoc.SelectSingleNode("Books");
                xn.RemoveAll();
                foreach (Book b in books)
                {
                    XmlNode xTop = xDoc.CreateElement("Book");
                    XmlNode xTitle = xDoc.CreateElement("Title");
                    XmlNode xAuthor = xDoc.CreateElement("Author");
                    XmlNode xGenre = xDoc.CreateElement("Genre");
                    XmlNode xISBN = xDoc.CreateElement("ISBN");
                    XmlNode xCopies = xDoc.CreateElement("Copies");
                    xTitle.InnerText = b.Title;
                    xAuthor.InnerText = b.Author;
                    xGenre.InnerText = b.Genre;
                    xISBN.InnerText = b.ISBN;
                    xCopies.InnerText = b.Copies.ToString();
                    xTop.AppendChild(xTitle);
                    xTop.AppendChild(xAuthor);
                    xTop.AppendChild(xGenre);
                    xTop.AppendChild(xISBN);
                    xTop.AppendChild(xCopies);
                    xDoc.DocumentElement.AppendChild(xTop);
                    xDoc.Save("\\BookStore\\books.xml");

                }




            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                // this button changes the values from the book which already exist.

                books[listView1.SelectedItems[0].Index].Title = textBox1.Text;
                books[listView1.SelectedItems[0].Index].Author = textBox2.Text;
                books[listView1.SelectedItems[0].Index].Genre = comboBox1.Text;
                books[listView1.SelectedItems[0].Index].ISBN = textBox4.Text;
                books[listView1.SelectedItems[0].Index].Copies = int.Parse(comboBox2.Text.ToString());

                repeat();

              

                XmlDocument xDoc2 = new XmlDocument();
                xDoc2.Load("\\BookStore\\books.xml");

                foreach (XmlNode xn1 in xDoc2.SelectNodes("Books/book"))
                {
                    Book w = new Book();
                    w.Title = xn1.SelectSingleNode("Title").InnerText;
                    w.Author = xn1.SelectSingleNode("Author").InnerText;
                    w.Genre = xn1.SelectSingleNode("Genre").InnerText;
                    w.ISBN = xn1.SelectSingleNode("ISBN").InnerText;
                    w.Copies = int.Parse(xn1.SelectSingleNode("Copies").InnerText);
                    books.Add(w);

                    ListViewItem list1 = new ListViewItem(w.Title);
                    list1.SubItems.Add(w.Author);
                    list1.SubItems.Add(w.Genre);
                    list1.SubItems.Add(w.ISBN);
                    list1.SubItems.Add(w.Copies.ToString());

                    listView1.Items.Add(list1);


                }
            }
            catch { }
                



            
            



            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //This button adds text into list view then xml files takes it from list view and text boxes.
            Book b = new Book();
            b.Title = textBox1.Text;
            b.Author = textBox2.Text;
            b.Genre = comboBox1.Text;
            b.ISBN = textBox4.Text;
            b.Copies = int.Parse(comboBox2.Text.ToString());
            books.Add(b);
            ListViewItem list = new ListViewItem(b.Title);
            list.SubItems.Add(b.Author);
            list.SubItems.Add(b.Genre);
            list.SubItems.Add(b.ISBN);
            list.SubItems.Add(b.Copies.ToString());
            listView1.Items.Add(list);
            repeat();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;



        }


        

        void Remove()
        {
            // this function deletes the information just for the event not for the storage. I couldnt make a full function for both(i mean xml and right away)
            try
            {
                listView1.Items.Remove(listView1.SelectedItems[0]);
                books.RemoveAt(listView1.SelectedItems[0].Index);
            }
            catch { }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you really want to delete this ?", string.Empty, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Remove();
            }
            else
            {
                return;
            }
   
            


                {
                
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load("\\BookStore\\books.xml");
                    foreach (XmlNode node in xdoc.SelectNodes("Books/Book"))
                    {
                        if (node.SelectSingleNode("Title").InnerText == textBox1.Text)
                        {
                            node.ParentNode.RemoveChild(node);
                        }

                    }

                    xdoc.Save("\\BookStore\\books.xml");
                }
                catch { }
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.SelectedIndex = 0;
                textBox4.Text = "";
                comboBox2.SelectedIndex = 0;

            }

        }

        private void AddBooks_Load(object sender, EventArgs e)
        {
            // when we click manage people in main program, add people loads and start with creating xml file then fill it with information which come from class Book and textboxes.
            comboBox1.SelectedIndex = 0;


            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            if (!Directory.Exists("\\BookStore"))

                Directory.CreateDirectory("\\BookStore");

            if (!File.Exists("\\BookStore\\books.xml"))
            {
                XmlTextWriter xW = new XmlTextWriter("\\BookStore\\books.xml", Encoding.UTF8);
                xW.WriteStartElement("Books");
                xW.WriteEndElement();
                xW.Close();
            }
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("\\BookStore\\books.xml");
            foreach (XmlNode xn in xDoc.SelectNodes("Books/Book"))
            {
                Book b = new Book();
                b.Title = xn.SelectSingleNode("Title").InnerText;
                b.Author = xn.SelectSingleNode("Author").InnerText;
                b.Genre = xn.SelectSingleNode("Genre").InnerText;
                b.ISBN = xn.SelectSingleNode("ISBN").InnerText;
                b.Copies = int.Parse(xn.SelectSingleNode("Copies").InnerText);
                books.Add(b);
                ListViewItem list = new ListViewItem(b.Title);
                list.SubItems.Add(b.Author);
                list.SubItems.Add(b.Genre);
                list.SubItems.Add(b.ISBN);
                list.SubItems.Add(b.Copies.ToString());
                listView1.Items.Add(list);


            }
        }

        private void AddBooks_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

      

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }



        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            // this is list of books and their information which keep in the list. List only shows title, when you click on the title you will see the full details of the book.
            try
            {
                textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
                textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
                comboBox1.Text = listView1.SelectedItems[0].SubItems[2].Text;
                textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
                comboBox2.Text = listView1.SelectedItems[0].SubItems[4].Text;

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

  

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();//clear list
            // when we click manage people in main program, add books loads and start with creating xml file then fill it with information which come from class person and textboxes.
            //comboBox1.SelectedIndex = 0;

            foreach (Book b in books)//instead of loading XML file again and again, loop for each book and Add to ListView
            {

                ListViewItem list = new ListViewItem(b.Title);
                list.SubItems.Add(b.Author);
                list.SubItems.Add(b.Genre);
                list.SubItems.Add(b.ISBN);
                list.SubItems.Add(b.Copies.ToString());
                listView1.Items.Add(list);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.TextLength ==0)
                {
                    listView1.Items.Clear();//clear list
                    // when we click manage people in main program, add books loads and start with creating xml file then fill it with information which come from class person and textboxes.
                    //comboBox1.SelectedIndex = 0;

                    foreach (Book b in books)//instead of loading XML file again and again, loop for each book and Add to ListView
                    {

                        ListViewItem list = new ListViewItem(b.Title);
                        list.SubItems.Add(b.Author);
                        list.SubItems.Add(b.Genre);
                        list.SubItems.Add(b.ISBN);
                        list.SubItems.Add(b.Copies.ToString());
                        listView1.Items.Add(list);
                    }
                }
                else
                {
                    int selection = comboBox3.SelectedIndex;//comboBox3 is drop down with Author, genre, etc...
                    ArrayList results = new ArrayList();//initialize arraylist
                    for (int i = 0; i < listView1.Items.Count; i++)//loops rows of ListView1 
                    {
                        string a = listView1.Items[i].SubItems[selection].Text;//creates a string based on row i, column 'selection'
                        if (a.IndexOf(textBox5.Text, StringComparison.OrdinalIgnoreCase) != -1)//compares user types in textBox5 (search) ignores case
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



