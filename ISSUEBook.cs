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
    public partial class ISSUEBook : Form
    {
        public ISSUEBook()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=MyLipraryDB;Integrated Security=TrueData Source=.;Initial Catalog=MyLipraryDB;Integrated Security=True;Connect Timeout=30");
        private void FillStudent()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select Stdid from StudentTbl", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Stdid", typeof(int));
            dt.Load(rdr);
            StdCb.ValueMember = "Stdid";
            StdCb.DataSource = dt;
            con.Close();
        }
        private void FillBook()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qey>"+0+"", con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(rdr);
            Bookcb.ValueMember = "BookName";
            Bookcb.DataSource = dt;
            con.Close();
        }
        public void populate()
        {
            con.Open();
            String query = "Select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueBookDGV.DataSource = ds.Tables[0];
            con.Close();
        }
        private void fetchstddata()
        {
            con.Open();
            string query = " select * from StudentTbl where Stdid =" + StdCb.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                stdnameTb.Text = dr["StdName"].ToString();
                stddpmntTb.Text = dr["StdDep"].ToString();
                PhoneTb.Text = dr["StdPhone"].ToString();
            }
            con.Close();
        }
        private void UpdateBook()
        {
            int Qey,newQey;

            con.Open();
            string query = " select * from BookTbl where BookName = '" + Bookcb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qey = Convert.ToInt32(dr["Qey"].ToString());
                newQey = Qey - 1;
                string query1 = " update BookTbl set Qey=" + newQey + " where BookName='" + Bookcb.SelectedValue.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.ExecuteNonQuery();

            }con.Close();
        }
        private void UpdateBookCancellation()
        {
            int Qey, newQey;

            con.Open();
            string query = " select * from BookTbl where BookName = '" + Bookcb.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qey = Convert.ToInt32(dr["Qey"].ToString());
                newQey = Qey + 1;
                string query1 = " update BookTbl set Qey=" + newQey + " where BookName='" + Bookcb.SelectedItem.ToString() + "';";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.ExecuteNonQuery();

            }
            con.Close();
        }
        private void ISSUEBook_Load(object sender, EventArgs e)
        {
            FillStudent();
            FillBook();
            populate();
            IssueNumTb.Enabled = false;
        }

        private void bunifuButton5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                Application.Exit();

            }
        }

        private void bunifuButton6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }

    
        private void StdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstddata();
        }

        private void bunifuButton4_Click(object sender, EventArgs e)
        {
            if ( stdnameTb.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
               // string issuedate = IssueDate.Value.Day.ToString() + "/" + IssueDate.Value.Month.ToString() +"/"+ IssueDate.Value.Year.ToString();
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into IssueTbl values('" + StdCb.SelectedValue.ToString() + "','" + stdnameTb.Text + "','" + stddpmntTb.Text + " ','"+ PhoneTb.Text+" ','"+Bookcb.SelectedValue.ToString() +"',' " + IssueDate.Value + "')", con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book Successfuly Issued");
                con.Close();
                UpdateBook();
                populate();
            }
        }

        private void bunifuButton3_Click(object sender, EventArgs e)
        {
            if (IssueNumTb.Text == "")
            {
                MessageBox.Show("Enter The Issue Number");
            }
            else
            {
                con.Open();
                String query = "delete from IssueTbl Where IssueNum =" + IssueNumTb.Text + ";";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Issue Successfully Canceled");
                con.Close();
                UpdateBookCancellation();
                populate();
            } 
        }

        private void IssueBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            IssueNumTb.Text = IssueBookDGV.SelectedRows[0].Cells[0].Value.ToString();
            StdCb.SelectedItem = IssueBookDGV.SelectedRows[0].Cells[1].Value.ToString();
            stdnameTb.Text = IssueBookDGV.SelectedRows[0].Cells[2].Value.ToString();
            stddpmntTb.Text = IssueBookDGV.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTb.Text = IssueBookDGV.SelectedRows[0].Cells[4].Value.ToString();
            Bookcb.Text = IssueBookDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void bunifuButton10_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }
    }
}
