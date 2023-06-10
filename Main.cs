using Bunifu.UI.WinForms;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
         }

        private void LipBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Librarian librarian = new Librarian();
            librarian.Show();   
        }

        private void StudentBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Student student = new Student();
            student.Show(); 
        }

        private void IssueBtn_Click(object sender, EventArgs e)
        {
            ISSUEBook book = new ISSUEBook();
            book.Show();
            this.Hide();

            //this.Hide();    
            //ISSUEBook book = new ISSUEBook();
            //book.Show();
        }

        private void ReturnBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReturnBook returnBook = new ReturnBook();
            returnBook.Show();  
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Books books= new Books();
            books.Show();
        }

        private void bunifuButton8_Click(object sender, EventArgs e)
        {

            this.Hide();
            ISSUEBook book = new ISSUEBook();
            book.Show();
        }

        private void bunifuButton7_Click(object sender, EventArgs e)
        {
            AboutUs about = new AboutUs();
            about.Show();
        }

        private void bunifuButton9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
                this.WindowState = FormWindowState.Minimized;
        }

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=MyLipraryDB;Integrated Security=TrueData Source=.;Initial Catalog=MyLipraryDB;Integrated Security=True;Connect Timeout=30");

        private void Main_Load(object sender, EventArgs e)
        {
            con.Open();

            SqlDataAdapter sdal = new SqlDataAdapter("select count(*) from BookTbl", con);
            DataTable dt = new DataTable();
            sdal.Fill(dt);
            Booktbl.Text = dt.Rows[0][0].ToString();

            SqlDataAdapter sdal2 = new SqlDataAdapter("select count(*) from StudentTbl", con);
            DataTable dt2 = new DataTable();
            sdal2.Fill(dt2);
            Studentstbl.Text = dt2.Rows[0][0].ToString();

            SqlDataAdapter sdal3 = new SqlDataAdapter("select count(*) from LibrarianTbl", con);
            DataTable dt3 = new DataTable();
            sdal3.Fill(dt3);
            Librariantbl.Text = dt3.Rows[0][0].ToString();

            SqlDataAdapter sdal4 = new SqlDataAdapter("select count(*) from ReturnTb", con);
            DataTable dt4 = new DataTable();
            sdal4.Fill(dt4);
            ReturnTbl.Text = dt4.Rows[0][0].ToString();

            SqlDataAdapter sdal5 = new SqlDataAdapter("select count(*) from IssueTbl", con);
            DataTable dt5 = new DataTable();
            sdal5.Fill(dt5);
            IssueTbl.Text = dt5.Rows[0][0].ToString();

            con.Close();
        }
    }
}
